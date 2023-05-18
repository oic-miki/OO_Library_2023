using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace OO_C_Sharp_WinFormsApp
{

    /// <summary>
    /// 登録済み利用者
    /// </summary>
    public abstract partial class RegisteredUserList : Form
    {

        private UserDataBase userDB = UserDataBase.get();

        public RegisteredUserList()
        {

            InitializeComponent();

            SuspendLayout();

            method4Test(); // テスト用のメソッド（授業用） ※リリース時に削除（もしくはモックオブジェクトで実装）

            initialize();

            /*
             * フォーム
             */
            Text = "登録済み利用者";

            // ドラッグ＆ドロップを実行可能にする
            initializeDragDrop();

            ResumeLayout(false);

            PerformLayout();

        }

        private void initializeDragDrop()
        {

            DragOver += event_DragOver;
            DragDrop += event_DragDrop;
            DragEnter += event_DragEnter;
            AllowDrop = true;

        }

        private void event_DragOver(object sender, DragEventArgs e)
        {

            e.Effect = DragDropEffects.Move;

        }

        private void event_DragDrop(object sender, DragEventArgs e)
        {

            object obj = e.Data.GetData(DataFormats.Serializable);
            if (obj is PersonPanel)
            {

                PersonPanel personPanel = (obj as PersonPanel);

                if (!Controls.Contains(personPanel))
                {

                    Controls.Add(personPanel);

                }

                e.Effect = DragDropEffects.Move;

            }

        }

        private void event_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.Serializable))
            {

                e.Effect = DragDropEffects.Move;

            }
            else
            {

                e.Effect = DragDropEffects.None;

            }

        }

        /*
         * テスト用メソッド
         */
        private void method4Test()
        {

//            getUserDB().save(NullPerson.get());

//            getUserDB().removeAll();

        }

        private void initialize()
        {

            removeAll();

            int userCount = getUserDB().count();

            if (userCount > 0)
            {

                int x = 20;
                int y = 20;
                int incrementalValueOfX = 800 / userCount - 100;
                int incrementalValueOfY = 450 / userCount - 50;

                foreach (Person user in getUserDB().list())
                {

                    PersonPanel personPanel = new PersonPanel(user);

                    Controls.Add(personPanel);

                    personPanel.setLocation(x, y).bringToFront(); // コントロールの追加時に最前面へ配置すると効果がないので追加後にする

                    x += incrementalValueOfX;
                    y += incrementalValueOfY;

                }

            }

        }

        protected UserDataBase getUserDB()
        {

            return userDB;

        }

        /// <summary>
        ///  Makes the control display by setting the visible property to true
        /// </summary>
        public RegisteredUserList show()
        {

            // ここに表示前の処理を記述できます

            // 表示する
            Show();

            return this;

        }

        /// <summary>
        ///  Brings this control to the front of the zorder.
        /// </summary>
        public RegisteredUserList bringToFront()
        {

            // 最前面に配置する
            BringToFront();

            return this;

        }

        /// <summary>
        /// リストの件数を返します。
        /// </summary>
        /// <returns></returns>
        public int count()
        {

            return Controls.Count;

        }

        /// <summary>
        /// リストが空かどうかを返します。
        /// </summary>
        /// <returns>空の場合 true</returns>
        public bool isEmpty()
        {

            return count() == 0;

        }

        /// <summary>
        /// すべてのデータを破棄します。
        /// </summary>
        /// <returns></returns>
        private RegisteredUserList removeAll()
        {

            int removeCount = count();

            while (removeCount-- > 0)
            {

                Controls.RemoveAt(removeCount);

            }

            return this;

        }

    }

    public class PlaceRegisteredUserList : RegisteredUserList
    {

        public RegisteredUserList save(UserPlaceRegister userPlaceRegister)
        {

            Debug.Assert(userPlaceRegister != null);

            getUserDB().save(userPlaceRegister.getUser());

            return this;

        }

        public RegisteredUserList saveTemporary(UserPlaceRegister userPlaceRegister)
        {

            Debug.Assert(userPlaceRegister != null);

            // ※一時保存は未実装

            return this;

        }

    }

}