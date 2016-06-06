using UnityEngine;
using System.Collections;


    public class DataFacade
    {
        DataMetricObstacle dmo = new DataMetricObstacle();
        DataMetricGame dmg = new DataMetricGame();
        DataMetricAttack dma = new DataMetricAttack();
       
       

        //class obstacle
        public enum Obstacle
        {
            Monkey, FlyingEnemy, Lavaman,
            Panther, Snake, Snowman, FallingRock, FireFinish, Geyser, EndBoss,
            IcePool, Icicle, Lavafall, RollingBoulder, RollingBoulderSurprise, Coconut, LavaBall, SnowBall
        }

        public Obstacle obstacle;
        public string spawnTime;
        public string defeatedTime;
        public string howItDied;
        public int gameID; //for obstacle and attack

        //class Game
        public enum Level { Tutorial, Fire1, Fire2, Fire3, Ice1, Ice2, Ice3, Jungle1, Jungle2, Jungle3 }

        public int session;
        public string starttime;
        public string endTime;
        public Level level;
        public int playerDied;
        public string howPlayerDied;
         
        //class Attack
        public enum Type { Fire, Ice, Telekinesis, Destruction }

        public string attackTime;
        public Type type;

        //depending the type of data got you acess to one or other class
        public void saveLocalData()
        {
            if(spawnTime != null)
            {
                dmo.saveLocalData();
            }

            else if( session != 0)
            {
                dmg.saveLocalData();
            }

            else
            {
                dma.saveLocalData();
            }
        }
    }
