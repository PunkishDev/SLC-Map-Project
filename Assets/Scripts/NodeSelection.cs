using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pathfinding;

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
        if (!string.IsNullOrEmpty(startInput.text) && !string.IsNullOrEmpty(endInput.text))
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
                Seeker s = pathfindingGuy.GetComponent<Seeker>();

                Path currPath = s.StartPath(selectedStart.nodePosition.position, selectedEnd.nodePosition.position);
                
                pathfindingGuy.transform.position = selectedStart.nodePosition.position;

                pathfindingGuy.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
        }
    }
}
