using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public abstract bool run();
}

public class IsTrue : Task
{
    bool varToTest;

    public IsTrue(bool myBool)
    {
        varToTest = myBool;
    }

    public override bool run()
    {
        return varToTest;
    }
}

public class IsFalse : Task
{
    bool varToTest;

    public IsFalse(bool myBool)
    {
        varToTest = myBool;
    }

    public override bool run()
    {
        return !varToTest;
    }
}

public class OpenDoor : Task
{
    Door myDoor;

    public OpenDoor(Door door)
    {
        myDoor = door;
    }

    public override bool run()
    {
        return myDoor.Open();
    }
}

public class BargeDoor : Task
{
    Rigidbody myDoorRB;

    public BargeDoor(Rigidbody door)
    {
        myDoorRB = door;
    }

    public override bool run()
    {
        myDoorRB.AddForce(-10f, 0, 0, ForceMode.VelocityChange);
        return true;
    }
}

public class MoveTo : Task
{
    public GameObject myPosition;
    public GameObject treasurePosition;
    public float nearDistance = 1; 

    public MoveTo(GameObject mover, GameObject target)
    {
        myPosition = mover;
        treasurePosition = target;
    }
    public override bool run()
    {
        while ((treasurePosition.transform.position - myPosition.transform.position).magnitude > nearDistance)
        {
            myPosition.transform.position = Vector3.MoveTowards(myPosition.transform.position, treasurePosition.transform.position, 2 * Time.deltaTime);

        }
        return true;
    }

}

public class Sequence : Task
{
    List<Task> children;

    public Sequence(List<Task> taskList)
    {
        children = taskList;
    }
    public override bool run()
    {

        foreach (Task c in children)
        {
            if (!c.run())
            {
                return false;
            }
        }
        return true;
    }
}
public class Selector : Task
{
    List<Task> children;

    public Selector(List<Task> taskList)
    {
        children = taskList;
    }
    public override bool run()
    {
        foreach (Task c in children)
        {
            if (c.run())
            {
                return true;
            }
        }
        return false;
    }
}
