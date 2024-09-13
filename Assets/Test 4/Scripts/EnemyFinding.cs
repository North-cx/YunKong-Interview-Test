using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyFinding : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask clickMask;
    public Grid grid;
    public float moveSpeed = 5f; // 移动速度
    private Queue<Vector3> pathPoints = new Queue<Vector3>(); // 路径点队列
    private Vector3 currentTarget;
    public bool isMoving;

    public float attackRange = 1f;  // 攻击范围
    public float attackInterval = 1f;  // 攻击间隔
    private float lastAttackTime;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentTarget = transform.position;
    }

    private void Update()
    {
        if (Time.time - lastAttackTime > attackInterval)
        {
            FindEnemy();
        }

        MoveAlongPath();

        if (isMoving)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void FindEnemy()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (var enemy in enemies)
        {
            FindPath(transform.position, enemy.transform.position);

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

            if (Vector3.Distance(transform.position, enemy.transform.position) <= attackRange)
            {
                enemy.OnAttack();
                animator.SetBool("isMoving", false);
                animator.SetTrigger("attack");
                lastAttackTime = Time.time;
                break;
            }
        }
    }

    void MoveAlongPath()
    {
        // 移动到目标点
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            if (pathPoints.Count > 0)
            {
                currentTarget = pathPoints.Dequeue();
                currentTarget.y = transform.position.y;
            }
            else
            {
                isMoving = false;
            }
        }
        else
        {
            Vector3 direction = (currentTarget - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);
            isMoving = true;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
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

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
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

