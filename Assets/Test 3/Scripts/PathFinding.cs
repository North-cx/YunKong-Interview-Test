using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Camera mainCamera;
    public Transform seeker;
    public LayerMask clickMask;
    Grid grid;
    public float moveSpeed = 5f; // 移动速度
    private Queue<Vector3> pathPoints = new Queue<Vector3>(); // 路径点队列
    private Vector3 currentTarget;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        currentTarget = seeker.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickMask))
            {
                Vector3 worldPosition = hit.point;
                FindPath(seeker.position, worldPosition);

                Debug.Log("鼠标点击的位置: " + worldPosition);
                if (grid.path.Count > 0)
                {
                    if (pathPoints != null)
                    {
                        pathPoints.Clear();
                    }
                    foreach (Node n in grid.path)
                    {
                        pathPoints.Enqueue(n.worldposition);
                    }
                }
            }
        }

        MoveAlongPath();

    }

    void MoveAlongPath()
    {
        // 移动到目标点
        if (Vector3.Distance(seeker.transform.position, currentTarget) < 0.01f)
        {
            if (pathPoints.Count > 0)
            {
                currentTarget = pathPoints.Dequeue();
            }
        }
        else
        {
            seeker.transform.position = Vector3.MoveTowards(seeker.transform.position, currentTarget, moveSpeed * Time.deltaTime);
        }
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node starNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(starNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(starNode, targetNode);
                return;
            }

            foreach(Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.Parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();

        grid.path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }

    }
}
