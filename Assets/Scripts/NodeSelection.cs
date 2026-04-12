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

                RenderLine();
            }
        }
    }

    private void RenderLine()
    {
        LineRenderer lr = GetComponent<LineRenderer>();

        lr.enabled = true;

        lr.positionCount = 0;

        lr.SetPositions(new Vector3[] { selectedStart.nodePosition.position});

        StartCoroutine(addNextLinePoint(lr));
    }

    private IEnumerator addNextLinePoint(LineRenderer lr)
    {
        while (pathfindingGuy.transform.position != selectedEnd.nodePosition.position)
        {
            lr.positionCount++;

            Vector3[] positions = new Vector3[lr.positionCount];

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = lr.GetPosition(i);
                
                if (i == lr.positionCount - 1)
                {
                    positions[i] = new Vector3(pathfindingGuy.transform.position.x, pathfindingGuy.transform.position.y, 0);
                }else
                {
                    positions[i] = lr.GetPosition(i);
                }
            }

            lr.SetPositions(positions);
            
            yield return new WaitForSeconds(0.125f);
        }

        lr.enabled = true;
    }
}
