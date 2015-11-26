using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using ShootingSharp.core;

namespace ShootingSharp.movie
{
    public class MovieManager
    {
        private const string DefaultMovieFolderName = "movie"; 

        private static MovieManager instance;

        private Dictionary<string, int> movieDictionary;
        private bool flag;
        private int handle;

        private MovieManager()
        {
            this.movieDictionary = new Dictionary<string, int>();
            this.flag = true;
        }

        public static MovieManager GetInstance()
        {
            if (instance == null)
                instance = new MovieManager();

            return instance;
        }

        public bool PlayMovie(string name, int extend)
        {
            if (flag)
            {

                if (this.movieDictionary.ContainsKey(name))
                {
                    DX.PlayMovieToGraph(this.movieDictionary[name]);
                }
                else
                {
                    this.LoadMovie(name);
                    DX.PlayMovieToGraph(this.movieDictionary[name]);
                }

                this.handle = this.movieDictionary[name];
                this.flag = false;
            }

          DX.DrawGraph(0, 100, this.handle, DX.FALSE);

          if (DX.GetMovieStateToGraph(this.handle) > 0)
              return true;
          else
              return false;
        }

        private void LoadMovie(string name)
        {
          
            Logger.GetInstance().Debug("movie file loading... : " + name);

            int movieHandle = DX.LoadGraph(FileUtility.FindTextureDirectory(DefaultMovieFolderName) + "\\" + name);

            Logger.GetInstance().Debug(name + " is load");

            this.movieDictionary.Add(name, movieHandle);
        }

        public void StopMovie()
        {
            this.flag = true;

            DX.PauseMovieToGraph(handle);

            DX.SeekMovieToGraph(handle, 0);
        }
    }
}
