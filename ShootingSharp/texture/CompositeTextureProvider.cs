using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ShootingSharp.core;
using System.IO;
using DxLibDLL;

namespace ShootingSharp.texture
{
    /// <summary>
    /// DxLib高速描画のための複合画像を提供する
    /// </summary>
    public class CompositeTextureProvider
    {
        private struct GraphData
        {
            public Size GraphSize { get; set; }
            public Point LeftUpPoint { get; set; }

            public GraphData(Size s, Point p) : this()
            {   
                this.GraphSize = s;
                this.LeftUpPoint = p;
            }
        }

        private struct BitmapData
        {
            public Bitmap Graph { get; set; }
            public string Name { get; set; }

            public BitmapData(Bitmap b, string name)
                : this()
            {
                this.Graph = b;
                this.Name = name;
            }
        }

        private const string CompositeGraphName = "composite_graph.png";
        private const string DefaultTextureDirectoryName = "texture";
        private const string CompositeTextureDirectoryName = "composite";
        private const int XMargin = 10;
        private const int YMargin = 10;


        private Dictionary<string, GraphData> imgDic = new Dictionary<string, GraphData>();

        private Logger logger = Logger.GetInstance();

        private int compositeGraphHandle;

        private List<Point> widthList = new List<Point>();
        private int maxY;
        private bool widthListFlag = true;

        public CompositeTextureProvider()
        {
            logger.Debug("Composite Graph Creating...");

            Bitmap compositeGraph = this.CreateCompositeGraph(DefaultTextureDirectoryName);
            string s = FileUtility.FindTextureDirectory(CompositeTextureDirectoryName);
            compositeGraph.Save(s + "\\" + CompositeGraphName, System.Drawing.Imaging.ImageFormat.Png);

            logger.Debug("Composite Graph Created");

            logger.Debug("Composite Graph Loading...");
            this.compositeGraphHandle = DX.LoadGraph(s + "\\" + CompositeGraphName);

            logger.Debug("Composite Graph Loaded");
        }

        /// <summary>
        /// 2つの画像を横に結合します
        /// </summary>
        /// <param name="img1"></param>
        /// <param name="img2"></param>
        /// <returns></returns>
        public Bitmap Chain(Bitmap img1, Bitmap img2)
        {
            int width = img1.Width + img2.Width;
            int height = img1.Height > img2.Height ? img1.Height : img2.Height;
            Bitmap img3 = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img3);
            g.DrawImage(img1, new Point(0, 0));
            g.DrawImage(img2, new Point(img1.Width, 0));
            g.Dispose();
            return img3;
        }

        /// <summary>
        /// フォルダ内の全ての画像を結合した複合画像を返します
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        public Bitmap CreateCompositeGraph(string dirName)
        {
            string path = FileUtility.FindTextureDirectory(dirName);

            string[] stemp = Directory.GetFiles(path, "*", System.IO.SearchOption.AllDirectories);

            if (stemp.Length == 0)
                return null;

            List<string> s2temp = stemp.ToList();

            s2temp.RemoveAll(s => !System.Text.RegularExpressions.Regex.IsMatch(Path.GetFileName(s), @".*shot.*"));

            string[] files = s2temp.ToArray();

            List<BitmapData> array = new List<BitmapData>();

            Bitmap img = new Bitmap(files[0]);
            array.Add(new BitmapData(img, Path.GetFileName(files[0])));
            

            if (files.Length == 1)
                return img;

            Bitmap temp2;


            for (int i = 1; i < files.Length; i++)
            {
                temp2 = new Bitmap(files[i]);

                array.Add(new BitmapData(temp2, Path.GetFileName(files[i])));

            }

            return this.ChainAll(array);

        }

        public int GetImgHandle(string name)
        {
            if (!this.imgDic.ContainsKey(name))
                return -1;

            GraphData g = this.imgDic[name];
            int h = DX.DerivationGraph(g.LeftUpPoint.X, g.LeftUpPoint.Y, g.GraphSize.Width + XMargin, g.GraphSize.Height + YMargin, this.compositeGraphHandle);
            return h;
        }

        public int[] GetDivGraph(string name, int xNum, int yNum, int allNum, int xSize, int ySize)
        {
            int handle = this.GetImgHandle(name);

            if (handle < 0)
                return null;

            int[] buf = new int[allNum];

            for (int i = 0; i < allNum; i++)
            { 
                buf[i] = DX.DerivationGraph((i % xNum) * xSize , 0 + (i / xNum) * ySize, xSize, ySize, handle);
            }

            return buf;
        }

        public int GetDerivationGraph(int posX, int posY, int sizeX, int sizeY, string name)
        {
            return this.GetImgHandle(name) > 0 ? DX.DerivationGraph(posX, posY, sizeX, sizeY, this.GetImgHandle(name)) : -1;
        }


        private Bitmap ChainAll(List<BitmapData> array)
        {
            var query = from b in array
                        orderby b.Graph.Height descending
                        select b;

            array = query.ToList();

           

            Bitmap img = new Bitmap(array[0].Graph.Width + XMargin, array[0].Graph.Height + YMargin);

            Graphics g = Graphics.FromImage(img);

            g.DrawImage(array[0].Graph, 0, 0);

            g.Dispose();


            imgDic.Add(array[0].Name, new GraphData(img.Size, new Point(0, 0)));

            int x = array[0].Graph.Width + XMargin;
            maxY = array[0].Graph.Height;

            array.RemoveAt(0);
            
            while (array.Count > 0)
            {
                int x2 = array[0].Graph.Width + x;
                int y = array[0].Graph.Height + YMargin;

                Bitmap img2 = new Bitmap(img.Width + array[0].Graph.Width + XMargin, maxY);

                Graphics g2 = Graphics.FromImage(img2);

                g2.DrawImage(img, 0, 0);

                g2.DrawImage(array[0].Graph, x, 0);

                this.imgDic.Add(array[0].Name, new GraphData(array[0].Graph.Size, new Point(x, 0)));

                array.RemoveAt(0);
                Put(g2, x, y, x2, array);
               /*
                if (widthList.Count >= 1)
                {

                    for (int i = 0; i < widthList.Count - 1; i++)
                    {
                        this.widthListFlag = false;
                        Put(g2, widthList[i + 1].X, widthList[i].Y, widthList[i].X, array);
                        this.widthListFlag = true;
                    }
                }*/
                
                g2.Dispose();
                img = (Bitmap)img2.Clone();
                x = x2 + XMargin;
            }

            return img;
        }

        private void Put(Graphics g, int x, int y,int width, List<BitmapData> array)
        {
            if (array.Count == 0)
                return;

            if (widthListFlag)
                widthList.Add(new Point(width + XMargin, y));

            bool b = false;

            for (int i = 0; i < array.Count; i++)
            {
                if (array[i].Graph.Height <= maxY - y && array[i].Graph.Width <= width - x)
                {
                    g.DrawImage(array[i].Graph, x, y);
                    this.imgDic.Add(array[i].Name, new GraphData(array[i].Graph.Size, new Point(x, y)));
                    y += array[i].Graph.Height + YMargin;
                    width = array[i].Graph.Width + x;
                    array.RemoveAt(i);
                    b = true;
                    break;
                }
            }

            if (b)
            {
                Put(g, x, y, width, array);
            }

            return;
        }

    }
}
