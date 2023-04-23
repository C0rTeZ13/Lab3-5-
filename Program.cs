using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private const int INF = int.MaxValue / 2;
    private int n; //количество вершин в орграфе
    //private int m; //количество дуг в орграфе
    private List<int>[] adj; //список смежности
    private List<int>[] weight; //вес ребра в орграфе
    private bool[] used; //массив для хранения информации о пройденных и не пройденных вершинах
    private int[] dist; //массив для хранения расстояния от стартовой вершины
                        //массив предков, необходимых для восстановления кратчайшего пути из стартовой вершины
    private int[] pred;
    int start; //стартовая вершина, от которой ищется расстояние до всех других
    public int Start
    {
        get
        {
            return start;
        }
        set
        {
            start = value;
        }
    }
    //процедура запуска алгоритма Дейкстры из стартовой вершины
    public void Dejkstra(int s)
    {
        dist[s] = 0; //кратчайшее расстояние до стартовой вершины равно 0
        for (int iter = 0; iter < n; ++iter)
        {
            int v = -1;
            int distV = INF;
            //выбираем вершину, кратчайшее расстояние до которого еще не найдено
            for (int i = 0; i < n; ++i)
            {
                if (used[i])
                {
                    continue;
                }
                if (distV < dist[i])
                {
                    continue;
                }
                v = i;
                distV = dist[i];
            }
            //рассматриваем все дуги, исходящие из найденной вершины
            for (int i = 0; i < adj[v].Count; ++i)
            {
                int u = adj[v][i];
                int weightU = weight[v][i];
                //релаксация вершины
                if (dist[v] + weightU < dist[u])
                {
                    dist[u] = dist[v] + weightU;
                    pred[u] = v;
                }
            }
            //помечаем вершину v просмотренной, до нее найдено кратчайшее расстояние
            used[v] = true;
        }
    }

    //процедура считывания входных данных с консоли
    public void ReadData()
    {
        n = 8; //считываем количество вершин графа
        //m = 12; //считываем количество ребер графа
        start = 1 - 1;

        //инициализируем списка смежности графа размером n
        adj = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            adj[i] = new List<int>();
        }

        //инициализируем массивы расстояний, предшественников и использованных вершин
        dist = new int[n];
        pred = new int[n];
        used = new bool[n];
        //инициализируем списки весов ребер графа размером n
        weight = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            weight[i] = new List<int>();
        }
        //считываем ребра графа
        adj[0].Add(1);
        weight[0].Add(1);
        adj[0].Add(2);
        weight[0].Add(13);
        adj[0].Add(3);
        weight[0].Add(6);
        adj[0].Add(5);
        weight[0].Add(11);
        adj[0].Add(6);
        weight[0].Add(15);

        adj[1].Add(0);
        weight[1].Add(1);
        adj[1].Add(2);
        weight[1].Add(5);
        adj[1].Add(3);
        weight[1].Add(5);
        adj[1].Add(4);
        weight[1].Add(1);
        
        adj[2].Add(0);
        weight[2].Add(13);
        adj[2].Add(1);
        weight[2].Add(5);
        adj[2].Add(7);
        weight[2].Add(19);

        adj[3].Add(0);
        weight[3].Add(6);
        adj[3].Add(1);
        weight[3].Add(5);
        adj[3].Add(4);
        weight[3].Add(3);
        adj[3].Add(5);
        weight[3].Add(2);

        adj[4].Add(1);
        weight[4].Add(1);
        adj[4].Add(3);
        weight[4].Add(3);
        adj[4].Add(5);
        weight[4].Add(8);
        adj[4].Add(7);
        weight[4].Add(12);

        adj[5].Add(0);
        weight[5].Add(11);
        adj[5].Add(3);
        weight[5].Add(2);
        adj[5].Add(4);
        weight[5].Add(8);
        adj[5].Add(6);
        weight[5].Add(3);

        adj[6].Add(0);
        weight[6].Add(15);
        adj[6].Add(5);
        weight[6].Add(3);
        adj[6].Add(7);
        weight[6].Add(1);

        adj[7].Add(2);
        weight[7].Add(19);
        adj[7].Add(4);
        weight[7].Add(12);
        adj[7].Add(6);
        weight[7].Add(1);

        //инициализируем расстояние до всех вершин кроме стартовой бесконечностью
        for (int i = 0; i < n; i++)
        {
            if (i != start)
            {
                dist[i] = INF;
            }
        }
    }

    //процедура вывода результата
    public void PrintData()
    {
        for (int i = 0; i < n; ++i)
        {
            Console.WriteLine("Расстояние до точки {0}: {1}", i + 1, dist[i]);
            Console.Write("Кратчайший путь: ");
            int cur = i;
            List<int> path = new List<int>();
            while (cur != start)
            {
                path.Add(cur);
                cur = pred[cur];
            }
            path.Add(start);
            path.Reverse();
            Console.WriteLine(string.Join("->", path.Select(x => (x + 1).ToString())));
            Console.WriteLine();
        }
    }
}
public class Program {
    public static void Main()
    {
        Solution solution = new Solution();
        try
        {
            solution.ReadData();
            solution.Dejkstra(solution.Start);
            solution.PrintData();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.ReadLine();
    }
}