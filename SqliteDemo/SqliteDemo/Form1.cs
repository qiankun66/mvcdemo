using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SqliteDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
            InitializeComponent();
        }

        //数据库连接
        SQLiteConnection m_dbConnection;


        //创建一个空的数据库
        public void createNewDatabase()
        {
            try{
            SQLiteConnection.CreateFile("Steben.sqlite");
                }catch(Exception e)
            {
            }
        }

        //创建一个连接到指定数据库
        public void connectToDatabase()
        {
            m_dbConnection = new SQLiteConnection("Data Source=Steben.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        //在指定数据库中创建一个table
        public void createTable()
        {
            string sql = "create table highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //插入一些数据
        public void fillTable()
        {
            string sql = "insert into highscores (name, score) values ('Me', 3000)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //使用sql查询语句，并显示结果
        public void printHighscores()
        {
            string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string sqls = "Name: " + reader["name"] + "\tScore: " + reader["score"];
                Console.WriteLine(sqls);
            }
            Console.ReadLine();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createNewDatabase();
            connectToDatabase();
            createTable();
            fillTable();
            printHighscores();
        }
    }
}
