﻿using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OO_C_Sharp_WinFormsApp
{

    /// <summary>
    /// 利用者登録
    /// </summary>
    public partial class RegisterUser : Form
    {

        private Place place = NullPlace.get();
        private User user = NullUser.get();
        private Role role = Role.None;
        private PlaceRegister placeRegister = NullPersonPlaceRegister.get();
        private Status status = new Status();

        public RegisterUser(Place place)
        {

            Debug.Assert(place != null);

            this.place = place;

            Debug.Assert(this.place != null);

            InitializeComponent();

            initialize();

        }

        public RegisterUser(Place place, Role role)
        {

            Debug.Assert(place != null);
            Debug.Assert(role != null);

            this.place = place;
            this.role = role;

            Debug.Assert(this.place != null);
            Debug.Assert(this.role != null);

            InitializeComponent();

            initialize();

        }

        private void initialize()
        {

            if (role is not Role.None)
            {

                // 指定された役割で新規に利用者を作成する
                Controls.Add(new PersonPanel(user = new ExtendedUser(role)));

            }
            else
            {

                // 新規に利用者を作成する
                Controls.Add(new PersonPanel(user = new ExtendedUser()));

            }

            Text = "利用者登録";

        }

        /// <summary>
        ///  Makes the control display by setting the visible property to true
        /// </summary>
        public virtual RegisterUser show()
        {

            if (placeRegister is NullObject)
            {

                // データ登録用のオブジェクトを生成する
                // TODO ユーザーのオブジェクトで登録できるようにしたい（役割も保存するため）
                placeRegister = new PersonPlaceRegister(place, user).setStatus(status.addValue(SaveStatus.Temporary));

            }

            // 表示する
            Show();

            return this;

        }

        /// <summary>
        ///  Brings this control to the front of the zorder.
        /// </summary>
        public virtual RegisterUser bringToFront()
        {

            // 最前面に配置する
            BringToFront();

            return this;

        }

        /// <summary>
        /// 一時保存
        /// </summary>
        /// <returns></returns>
        public virtual RegisterUser save()
        {

            place.add(placeRegister);

            return this;

        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <returns></returns>
        public virtual RegisterUser register()
        {

            place.add(placeRegister.setStatus(status.addValue(SaveStatus.Complete)));

            return this;

        }

        /// <summary>
        /// 登録終了
        /// </summary>
        /// <returns></returns>
        public virtual RegisterUser finish()
        {

            Controls.RemoveAt(0);

            user = NullUser.get();

            return hide();

        }

        /// <summary>
        ///  Hides the control by setting the visible property to false;
        /// </summary>
        public virtual RegisterUser hide()
        {

            // 非表示にする
            Hide();

            return this;

        }

    }

    public class NullRegisterUser : RegisterUser, NullObject
    {

        private static RegisterUser registerUser = new NullRegisterUser();

        private NullRegisterUser() : base(NullPlace.get())
        {

        }

        public static RegisterUser get()
        {

            return registerUser;

        }

        public override RegisterUser show()
        {

            return this;

        }

        public override RegisterUser bringToFront()
        {

            return this;

        }

        public override RegisterUser register()
        {

            return this;

        }

        public override RegisterUser finish()
        {

            return this;

        }

        public override RegisterUser hide()
        {

            return this;

        }

    }

}