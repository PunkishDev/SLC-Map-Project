using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NodeSelection : MonoBehaviour
{
    public Node[] nodes;
    public InputField startInput;
    public InputField endInput;
    public GameObject pathfindingGuy;

    private Node selectedStart;
    private Node selectedEnd;


    public void FindPath()
    {
        if (!String.IsNullOrEmpty(startInput.text) && ! String.IsNullOrEmpty(endInput.text))
        {
            foreach (Node n in nodes)
            {
                if (startInput.text == n.roomNumber)
                {
                    selectedStart = n;
                }

                if (endInput.text == n.roomNumber)
                {
                    selectedEnd = n;
                }
            }

            if (selectedStart == null || selectedEnd == null) {
                Debug.Log("Selected Start or end room isn't available");
                return;
            }else
            {
                pathfindingGuy.transform.position = selectedStart.nodePosition.position;
                pathfindingGuy.GetComponent<AIDestinationSetter>().target = selectedEnd.nodePosition;
            }
        }
    }
}
