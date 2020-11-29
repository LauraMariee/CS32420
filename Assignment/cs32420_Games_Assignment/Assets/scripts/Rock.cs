using UnityEngine;

public class Rock : MonoBehaviour
{
    private struct RockState
    {
        public Vector2 Position { private set; get; }
        public bool IsTriggered { private set; get; }

        public RockState(Vector2 position, bool isTriggered)
        {
            Position = position;
            IsTriggered = isTriggered;
        }

        public override string ToString()
        {
            return $"pos={Position.ToString()} isTriggered={IsTriggered}";
        }
    }
    
    public int rockSpeed;
    
    private Rigidbody2D rigidbody;
    private bool rockTriggered;
    private readonly TimeTravel<RockState> timeTravel = new TimeTravel<RockState>();


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.GetChild(0).GetComponent<Rigidbody2D>();
        rockTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeTravel.IsTravellingBack)
        {
            if (rockTriggered)
            {
                rigidbody.velocity = new Vector2(0, -rockSpeed);
            }

            timeTravel.CaptureState(new RockState(rigidbody.position, rockTriggered));
        }
        else
        {
            var ttState = timeTravel.GetNextPastFrame();
            if (ttState.HasValue)
            {
                rigidbody.position = ttState.Value.Position;
                rockTriggered = ttState.Value.IsTriggered;
                if (!rockTriggered)
                {
                    rigidbody.Sleep();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !timeTravel.IsTravellingBack)
        {
            rockTriggered = true;
        }
    }

    public void TriggerTimeTravel(int duration)
    {
        timeTravel.StartToTravelBack(duration);
    }

}
