using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace PhoneApp3
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool Start_flg = false;
        private DateTime Start_time;
        private DateTime End_time;
        private int Diff_seconds;
        private int Try_time = 10;//最初は10秒から
        // コンストラクター
        public MainPage()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize(){
            //イニシャライズ
            this.Diff_seconds = 0;
            this.Start_flg = false;
            this.Txt_Msg.Text = string.Empty;
            this.Txt_Msg.Foreground = new SolidColorBrush(Colors.Red);//フォントの色を変えるやり方
            this.Txt_Msg.Text = Try_time + "秒チャレンジ！！";
            this.Btn_Start.Content = "START!";
            this.Btn_Continu.Visibility = Visibility.Collapsed;//非表示
            this.Btn_Start.Visibility = Visibility.Visible;//表示
        }
        //START&STOPボタン
        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            if (this.Start_flg){
                //STOP
                this.Start_flg = false;
                this.End_time = System.DateTime.Now;        //現在時刻の取得
                Result_msg();
                int seed = Environment.TickCount;
                Random rnd = new System.Random(seed++);     //乱数生成
                this.Try_time = rnd.Next(10,60);            //10から60の乱数生成
                this.Btn_Start.Visibility = Visibility.Collapsed;
                this.Btn_Continu.Visibility = Visibility.Visible;
            } else{
                //START
                //テーマを取得
                Visibility light = (Visibility)Resources["PhoneLightThemeVisibility"];
                Visibility dark = (Visibility)Resources["PhoneDarkThemeVisibility"];
                if (light == System.Windows.Visibility.Visible)
                {
                    // テーマはLight！！
                    this.Txt_Msg.Foreground = new SolidColorBrush(Colors.Black);
                }
                if (dark == System.Windows.Visibility.Visible)
                {
                    // テーマはDark！！
                    this.Txt_Msg.Foreground = new SolidColorBrush(Colors.White);
                }
                
                this.Start_flg = true;
                //ボタンのキャプションをSTOPにする。
                this.Btn_Start.Content = "STOP!";
                //メッセージをクリア
                this.Txt_Msg.Text = Try_time + "秒チャレンジ中・・・";
                //開始時刻を退避
                this.Start_time = System.DateTime.Now;
            }
        }
        //結果表示
        void Result_msg(){
            if ((this.End_time - this.Start_time).Seconds >120) {
              //OF対策
                this.Diff_seconds = 120;
                this.Txt_Msg.Text = "結果は2分以上でした。";
            }else{
                this.Diff_seconds = (this.End_time - this.Start_time).Seconds;
                this.Txt_Msg.Text = "結果は" + this.Diff_seconds + "秒でした。";
            }
            //各種メッセージ
            if (this.Diff_seconds == this.Try_time)
           {
               this.Txt_Msg.Text += "素晴らしい！！";              
           }
           else if (this.Diff_seconds < this.Try_time)
           {
               this.Txt_Msg.Text += "少し焦りすぎ！";
           }
           else if (this.Diff_seconds > this.Try_time && this.Diff_seconds < Try_time*2)
           {
               this.Txt_Msg.Text += "残念！！";
           }
           else
           {
               this.Txt_Msg.Text += "おっとりさんなんですね(^^)";
           }
            //リストボックスに表示
//            listBox1.Items.Reverse(); //逆順にソートして表示したい
            listBox1.Items.Add(Try_time + "秒チャレンジの結果：\r\n\t" + this.Txt_Msg.Text);
                    }
        //続けるボタン
        private void Btn_Continu_Click(object sender, RoutedEventArgs e)
        {
            Initialize();
        }
    }
}