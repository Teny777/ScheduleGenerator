using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Utils
{
    class Edge
    {
        public int b, u, c, f, back;
        public Edge(int b, int u, int c, int f, int back)
        {
            this.b = b;
            this.u = u;
            this.c = c;
            this.f = f;
            this.back = back;
        }
    }

    class MinMaxFlow
    {
        private List<List<Edge>> g = new List<List<Edge>>();
        public List<Edge> Edges { get; set; } = new List<Edge>();
        private int n, s, t;
        public void AddEdge(int a, int b, int u, int c)
        {
            Edge r1 = new Edge(b, u, c, 0, g[b].Count);
            Edge r2 = new Edge(a, 0, -c, 0, g[a].Count);
            g[a].Add(r1);
            g[b].Add(r2);
            Edges.Add(r1);
            Edges.Add(r2);
        }

        public MinMaxFlow(int n, int s, int t)
        {
            this.n = n;
            this.s = s;
            this.t = t;
            for (int i = 0; i <= n; ++i)
                g.Add(new List<Edge>());

            
        }

        public int Flow()
        {
            int flow = 0, cost = 0, INF = 1000000000, k = INF;
            while (flow < k)
            {
                List<int> id = new List<int>();
                List<int> d = new List<int>();
                List<int> q = new List<int>();
                List<int> p = new List<int>();
                List<int> p_rib = new List<int>();
                for (int i = 0; i < n; ++i)
                {
                    id.Add(0);
                    d.Add(INF);
                    p.Add(0);
                    q.Add(0);
                    p_rib.Add(0);
                }
                int qh = 0, qt = 0;
                q[qt++] = s;
                d[s] = 0;
                while (qh != qt)
                {
                    int v = q[qh++];
                    id[v] = 2;
                    if (qh == n) qh = 0;
                    for (int i = 0; i < g[v].Count; ++i)
                    {
                        Edge r = g[v][i];
                        if (r.f < r.u && d[v] + r.c < d[r.b])
                        {
                            d[r.b] = d[v] + r.c;
                            if (id[r.b] == 0)
                            {
                                q[qt++] = r.b;
                                if (qt == n) qt = 0;
                            }
                            else if (id[r.b] == 2)
                            {
                                if (--qh == -1) qh = n - 1;
                                q[qh] = r.b;
                            }
                            id[r.b] = 1;
                            p[r.b] = v;
                            p_rib[r.b] = i;
                        }
                    }
                }
                if (d[t] == INF) break;
                int addflow = k - flow;
                for (int v = t; v != s; v = p[v])
                {
                    int pv = p[v]; int pr = p_rib[v];
                    addflow = Math.Min(addflow, g[pv][pr].u - g[pv][pr].f);
                }
                for (int v = t; v != s; v = p[v])
                {
                    int pv = p[v]; int pr = p_rib[v], r = g[pv][pr].back;
                    g[pv][pr].f += addflow;
                    g[v][r].f -= addflow;
                    cost += g[pv][pr].c * addflow;
                }
                flow += addflow;
            }
            return flow;
        }

    }
}
