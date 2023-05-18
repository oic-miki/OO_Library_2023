using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace OO_C_Sharp_WinFormsApp
{

    /// <summary>
    /// �o�^�ςݗ��p��
    /// </summary>
    public abstract partial class RegisteredUserList : Form
    {

        private UserDataBase userDB = UserDataBase.get();

        public RegisteredUserList()
        {

            InitializeComponent();

            SuspendLayout();

            method4Test(); // �e�X�g�p�̃��\�b�h�i���Ɨp�j �������[�X���ɍ폜�i�������̓��b�N�I�u�W�F�N�g�Ŏ����j

            initialize();

            /*
             * �t�H�[��
             */
            Text = "�o�^�ςݗ��p��";

            // �h���b�O���h���b�v�����s�\�ɂ���
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
         * �e�X�g�p���\�b�h
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

                    personPanel.setLocation(x, y).bringToFront(); // �R���g���[���̒ǉ����ɍőO�ʂ֔z�u����ƌ��ʂ��Ȃ��̂Œǉ���ɂ���

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

            // �����ɕ\���O�̏������L�q�ł��܂�

            // �\������
            Show();

            return this;

        }

        /// <summary>
        ///  Brings this control to the front of the zorder.
        /// </summary>
        public RegisteredUserList bringToFront()
        {

            // �őO�ʂɔz�u����
            BringToFront();

            return this;

        }

        /// <summary>
        /// ���X�g�̌�����Ԃ��܂��B
        /// </summary>
        /// <returns></returns>
        public int count()
        {

            return Controls.Count;

        }

        /// <summary>
        /// ���X�g���󂩂ǂ�����Ԃ��܂��B
        /// </summary>
        /// <returns>��̏ꍇ true</returns>
        public bool isEmpty()
        {

            return count() == 0;

        }

        /// <summary>
        /// ���ׂẴf�[�^��j�����܂��B
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

            // ���ꎞ�ۑ��͖�����

            return this;

        }

    }

}