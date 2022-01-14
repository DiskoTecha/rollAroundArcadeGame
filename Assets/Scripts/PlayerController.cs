using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScoreController))]
public class PlayerController : MonoBehaviour
{
    public float acceleration = 0.5f; // Units/sec^2
    public float jumpStrength = 6.0f;
    [Range(0f, 0.3f)]
    public float stickyStrength = 0.02f; // Friction only when not accelerating using inputs
    public Vector3 resetPosition = new Vector3(0f, 3f, 0f);
    public Transform followCamera;
    public GameController gameController;

    private Vector3 currentVelocity;
    private Vector3 moveAxis;
    private Vector3 forceVector;
    private bool jump = false;
    private List<Transform> jumpableFloors;
    private int surfacesTouching;

    public Dictionary<string, PowerUp> powerUpsActing;
    private float defaultAcceleration;
    private float defaultJumpStrength;
    private float defaultStickyStrength;

    private int currentObjectiveAmount;
    private ScoreController scoreController;
    private int objectiveScoreAmount;
    private int defaultObjectiveScoreAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = new Vector3(0, 0, 0);
        moveAxis = new Vector3(0, 0, 0);
        forceVector = new Vector3(0, 0, 0);
        jumpableFloors = new List<Transform>();
        surfacesTouching = 0;

        defaultAcceleration = acceleration;
        defaultJumpStrength = jumpStrength;
        defaultStickyStrength = stickyStrength;
        powerUpsActing = new Dictionary<string, PowerUp>();

        currentObjectiveAmount = 0;
        scoreController = GetComponent<ScoreController>();
        objectiveScoreAmount = 10;
        defaultObjectiveScoreAmount = objectiveScoreAmount;
    }

    // Handle input in update
    void Update()
    {
        Vector3 actualForward = transform.position - followCamera.position;  // Difference between camera and player
        int reversed = actualForward.x < 0 ? -1 : 1;  // Accounts for angles greater than 180 degrees between the vectors in the dot product
        actualForward = new Vector3(actualForward.x, 0, actualForward.z); // Projection on x-z plane

        Vector3 assumedForward = new Vector3(0, 0, 1);  // Forward assumed by moveAxis setup

        // Angle between actual forward and assumed forward
        float angleToRotate = reversed * (180 / Mathf.PI) * Mathf.Acos(Vector3.Dot(actualForward, assumedForward) / (actualForward.magnitude * assumedForward.magnitude));

        moveAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));  // moveAxis vector in the assumed space
        moveAxis = Quaternion.Euler(0, angleToRotate, 0) * moveAxis.normalized;  // Apply rotation and normalize moveAxis vector to get moveAxis in actual global space

        forceVector = moveAxis * acceleration;

        if (Input.GetKeyDown("space") && jumpableFloors.Count > 0)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {   
        if (surfacesTouching > 0)
        {
            // Handle horizontal acceleration
            GetComponent<Rigidbody>().AddForce(forceVector.x, forceVector.y, forceVector.z, ForceMode.Acceleration);

            // Handle stickiness
            if (forceVector == Vector3.zero)
            {
                GetComponent<Rigidbody>().velocity *= (1 - stickyStrength);
            }
        }


        // Handle jump impulse
        if (jump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            jump = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        surfacesTouching += 1;

        ContactPoint[] contacts = new ContactPoint[collision.contactCount];

        collision.GetContacts(contacts);
        for (int i = 0; i < contacts.Length; i++)
        {
            if (Mathf.Acos(Vector3.Dot(contacts[i].normal, Vector3.up)) < Mathf.PI / 4)
            {
                jumpableFloors.Add(collision.transform);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        surfacesTouching -= 1;
        jumpableFloors.Remove(collision.transform);
    }

    public void KillPlayer()
    {
        ResetPlayerPosition();
        gameController.ResetGame();
        ResetPlayerValues();
        currentObjectiveAmount = 0;
        objectiveScoreAmount = defaultObjectiveScoreAmount;
        scoreController.ResetScore();
        powerUpsActing.Clear();
    }

    public void ResetPlayerPosition()
    {
        transform.position = resetPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void ResetPlayerValues()
    {
        acceleration = defaultAcceleration;
        jumpStrength = defaultJumpStrength;
        stickyStrength = defaultStickyStrength;
        followCamera.GetComponent<Follow>().ResetCamera();
    }

    private void LevelUp()
    {
        gameController.LevelUp();
        ResetPlayerPosition();
        objectiveScoreAmount += defaultObjectiveScoreAmount;
    }

    public bool AddPowerUp(PowerUp powerUp)
    {
        PowerUp actingPowerUp;
        if (powerUpsActing.TryGetValue(powerUp.gameObject.name, out actingPowerUp))
        {
            actingPowerUp.AddDuration();
            return false;
        }

        powerUpsActing.Add(powerUp.gameObject.name, powerUp);
        return true;
    }

    public void AddObjective()
    {
        if (++currentObjectiveAmount >= gameController.objectivesNeeded)
        {
            LevelUp();
            currentObjectiveAmount = 0;
        }

        scoreController.AddScore(objectiveScoreAmount);
    }

    public int GetCurrentObjectiveAmount()
    {
        return currentObjectiveAmount;
    }

    public void AddScore(int score)
    {
        scoreController.AddScore(score);
    }
}
