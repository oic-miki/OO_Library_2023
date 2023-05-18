﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_C_Sharp_WinFormsApp
{

    public abstract class BasePictureBox : PictureBox, ActionListener
    {

        private Person person;
        private List<Observer> observers = new List<Observer>();

        public BasePictureBox(Person person)
        {

            Debug.Assert(person != null);

            this.person = person;

            Debug.Assert(this.person != null);

        }

        protected Person getPerson()
        {

            return person;

        }

        public void listen(object sender)
        {

            if (sender is Event.Save)
            {

                // 声かけを伝搬する
                listen();

                // 変更を通知する
                notify();

            }

        }

        protected abstract void listen();

        private void notify()
        {

            // オブザーバーに更新を促す
            foreach (Observer observer in observers)
            {

                observer.update();

            }

        }

        public BasePictureBox addObserver(Observer observer)
        {

            Debug.Assert(observer != null);

            observers.Add(observer);

            Debug.Assert(observers.Contains(observer));

            return this;

        }

        public BasePictureBox setLocation(int x, int y)
        {

            // 表示位置を指定する
            Location = new Point(x, y);

            return this;

        }

    }

    public class PersonImagePictureBox : BasePictureBox
    {

        public PersonImagePictureBox(Person person) : base(person)
        {

            ((ISupportInitialize) this).BeginInit();

            BorderStyle = BorderStyle.Fixed3D;
            Name = "personImagePictureBox";
            TabStop = false;
            Image = getPerson().getImage();

            ((ISupportInitialize) this).EndInit();

        }

        protected override void listen()
        {

            // 最新の情報を設定する
            getPerson().addImage(Image);

        }

    }

}