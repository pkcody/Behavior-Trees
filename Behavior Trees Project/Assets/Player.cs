using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Door theDoor;
    public GameObject theTreasure;

    bool testBehavior = false;
    Task currTask;
    private void Start()
    {
        testBehavior = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!testBehavior)
            {
                testBehavior = true;
                currTask = StartTask();
                currTask.run();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    // Start is called before the first frame update
    Task StartTask()
    {
        // door locked > open
        List<Task> taskList = new List<Task>();

        Task isDoorNotLocked = new IsFalse(theDoor.isLocked);
        Task openDoor = new OpenDoor(theDoor);
        taskList.Add(isDoorNotLocked);
        taskList.Add(openDoor);
        Sequence openUnlockedDoor = new Sequence(taskList);

        // door still closed > barge
        taskList = new List<Task>();
        Task isDoorClosed = new IsFalse(theDoor.isOpen);
        Task bargeDoor = new BargeDoor(theDoor.transform.GetChild(0).GetComponent<Rigidbody>());
        taskList.Add(isDoorClosed);
        taskList.Add(bargeDoor);
        Sequence bargeClosedDoor = new Sequence(taskList);

        // open door
        taskList = new List<Task>();
        taskList.Add(openUnlockedDoor);
        taskList.Add(bargeClosedDoor);
        Selector openTheDoor = new Selector(taskList);

        // door closed
        taskList = new List<Task>();
        Task moveToDoor = new MoveTo(this.gameObject, theTreasure);
        Task moveToTreasure = new MoveTo(this.gameObject, theTreasure);
        taskList.Add(moveToDoor);
        taskList.Add(openTheDoor); // one way or another
        taskList.Add(moveToTreasure);
        Sequence getTreasureBehindClosedDoor = new Sequence(taskList);

        // door open
        taskList = new List<Task>();
        Task isDoorOpen = new IsTrue(theDoor.isOpen);
        taskList.Add(isDoorOpen);
        taskList.Add(moveToTreasure);
        Sequence getTreasureBehindOpenDoor = new Sequence(taskList);

        // move
        taskList = new List<Task>();
        taskList.Add(getTreasureBehindOpenDoor);
        taskList.Add(getTreasureBehindClosedDoor);
        Selector getTreasure = new Selector(taskList);

        return getTreasure;
    }
}
