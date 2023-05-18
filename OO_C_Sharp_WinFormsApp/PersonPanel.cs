using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;
using Label = System.Windows.Forms.Label;
using TextBox = System.Windows.Forms.TextBox;

namespace OO_C_Sharp_WinFormsApp
{

    public class PersonPanel : Panel, ISerializable
    {

        private List<Observer> observers = new List<Observer>();
        private List<ActionListener> actionListeners = new List<ActionListener>();

        public PersonPanel(Person person)
        {

            Debug.Assert(person != null);

            int tabIndex = 0;

            /*
             * ID
             */
            Controls.Add(createPersonIdTitle());
            Controls.Add(createPersonIdLabel(person));

            /*
             * 姓
             */
            Controls.Add(createFamilyNameTitle());
            FamilyNameLabel familyNameLabel = createFamilyNameLabel(person);
            Controls.Add(familyNameLabel);
            Controls.Add(createFamilyNameTextBox(person, tabIndex++).addObserver(familyNameLabel));

            /*
             * 名
             */
            Controls.Add(createPersonNameTitle());
            PersonNameLabel personNameLabel = createPersonNameLabel(person);
            Controls.Add(personNameLabel);
            Controls.Add(createPersonNameTextBox(person, tabIndex++).addObserver(personNameLabel));

            /*
             * 誕生日
             */
            PersonBirthdayDateTimePicker personBirthdayDateTimePicker = createPersonBirthdayDateTimePicker(person, tabIndex++);
            Controls.Add(personBirthdayDateTimePicker);

            /*
             * 年齢
             */
            Controls.Add(createAgeTitle());
            AgeLabel ageLabel = createAgeLabel(person);
            Controls.Add(ageLabel);
            personBirthdayDateTimePicker.addObserver(ageLabel);

            /*
             * イメージ
             */
            Controls.Add(createPersonImagePictureBox(person));

            /*
             * 役割
             */
            Controls.Add(createUserComboBox(person, tabIndex++));

            /*
             * 保存ボタン
             */
            Controls.Add(createSaveButton(tabIndex++));

            /*
             * パネル
             */
            Location = new Point(20, 20);
            ClientSize = new Size(500, 225);
            BorderStyle = BorderStyle.Fixed3D;
            Name = "personPanel";
            Text = person.fullName();

            // ドラッグ＆ドロップを実行可能にする
            initializeDragDrop();

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

            // NOP

        }

        private void initializeDragDrop()
        {

            MouseDown += event_MouseDown;

        }

        private void event_MouseDown(object sender, MouseEventArgs e)
        {

            if (DoDragDrop(this, DragDropEffects.Move) == DragDropEffects.Move)
            {

                setLocation(e.X, e.Y);

            }
            else if (e.Button == MouseButtons.Right)
            {

                // コンテキストメニューを表示する
                ContextMenuStrip.Show(this, e.X, e.Y);

            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref='System.Drawing.Point'/> class with the specified coordinates.
        /// </summary>
        public PersonPanel setLocation(int x, int y)
        {

            // 表示位置を指定する
            Location = new Point(x, y);

            return this;

        }

        /// <summary>
        ///  Brings this control to the front of the zorder.
        /// </summary>
        public PersonPanel bringToFront()
        {

            // 最前面に配置する
            BringToFront();

            return this;

        }

        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        private Title createPersonIdTitle()
        {
            
            Title title = new Title("ID");

            title.setLocation(30, 30).Size = new Size(38, 15);

            return title;

        }

        /// <summary>
        /// personIdLabel
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        private PersonIdLabel createPersonIdLabel(Person person)
        {

            PersonIdLabel personIdLabel = new PersonIdLabel(person);

            personIdLabel.setLocation(100, 30).Size = new Size(38, 15);

            // オブザーバーとして登録する
            addObserver(personIdLabel);

            return personIdLabel;

        }

        /// <summary>
        /// 姓
        /// </summary>
        /// <returns></returns>
        private Title createFamilyNameTitle()
        {

            Title title = new Title("姓");

            title.setLocation(30, 60).Size = new Size(38, 15);

            return title;

        }

        /// <summary>
        /// familyNameLabel
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        private FamilyNameLabel createFamilyNameLabel(Person person)
        {

            FamilyNameLabel familyNameLabel = new FamilyNameLabel(person);

            familyNameLabel.setLocation(100, 60).Size = new Size(38, 15);

            // オブザーバーとして登録する
            addObserver(familyNameLabel);

            return familyNameLabel;

        }

        /// <summary>
        /// familyNameTextBox
        /// </summary>
        /// <param name="person"></param>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        private FamilyNameTextBox createFamilyNameTextBox(Person person, int tabIndex)
        {

            FamilyNameTextBox familyNameTextBox = new FamilyNameTextBox(person);

            familyNameTextBox.setLocation(220, 58).Size = new Size(100, 23);
            familyNameTextBox.TabIndex = tabIndex;

            // イベントリスナーとして登録する
            addActionListener(familyNameTextBox);

            return familyNameTextBox;

        }

        /// <summary>
        /// 名
        /// </summary>
        /// <returns></returns>
        private Title createPersonNameTitle()
        {

            Title title = new Title("名");

            title.setLocation(30, 90).Size = new Size(38, 15);

            return title;

        }

        /// <summary>
        /// personNameLabel
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        private PersonNameLabel createPersonNameLabel(Person person)
        {

            PersonNameLabel personNameLabel = new PersonNameLabel(person);

            personNameLabel.setLocation(100, 90).Size = new Size(38, 15);

            // オブザーバーとして登録する
            addObserver(personNameLabel);

            return personNameLabel;

        }

        /// <summary>
        /// personNameTextBox
        /// </summary>
        /// <param name="person"></param>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        private PersonNameTextBox createPersonNameTextBox(Person person, int tabIndex)
        {

            PersonNameTextBox personNameTextBox = new PersonNameTextBox(person);

            personNameTextBox.setLocation(220, 88).Size = new Size(100, 23);
            personNameTextBox.TabIndex = tabIndex;

            // イベントリスナーとして登録する
            addActionListener(personNameTextBox);

            return personNameTextBox;

        }

        /// <summary>
        /// personBirthdayDateTimePicker
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        private PersonBirthdayDateTimePicker createPersonBirthdayDateTimePicker(Person person, int tabIndex)
        {

            PersonBirthdayDateTimePicker birthdayDateTimePicker = new PersonBirthdayDateTimePicker(person);

            birthdayDateTimePicker.setLocation(220, 118).Size = new Size(200, 23);
            birthdayDateTimePicker.TabIndex = tabIndex;

            // イベントリスナーとして登録する
            addActionListener(birthdayDateTimePicker);

            return birthdayDateTimePicker;

        }

        /// <summary>
        /// 年齢
        /// </summary>
        /// <returns></returns>
        private Title createAgeTitle()
        {

            Title title = new Title("年齢");

            title.setLocation(30, 120).Size = new Size(38, 15);

            return title;

        }

        /// <summary>
        /// ageLabel
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        private AgeLabel createAgeLabel(Person person)
        {

            AgeLabel ageLabel = new AgeLabel(person);

            ageLabel.setLocation(100, 120).Size = new Size(38, 15);

            return ageLabel;

        }

        /// <summary>
        /// personImagePictureBox
        /// </summary>
        /// <returns></returns>
        private PersonImagePictureBox createPersonImagePictureBox(Person person)
        {

            PersonImagePictureBox imagePictureBox = new PersonImagePictureBox(person);

            imagePictureBox.setLocation(360, 30).Size = new Size(60, 80);

            return imagePictureBox;

        }

        /// <summary>
        /// userComboBox
        /// </summary>
        /// <param name="person"></param>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        private ComboBox createUserComboBox(Person person, int tabIndex)
        {

            UserComboBox userComboBox;

            if (person is User)
            {

                userComboBox = new UserComboBox(person as User);

            }
            else
            {

                userComboBox = new UserComboBox();

            }

            Debug.Assert(userComboBox != null);

            userComboBox.setLocation(220, 30).Size = new Size(100, 23);
            userComboBox.TabIndex = tabIndex;

            return userComboBox;

        }

        /// <summary>
        /// saveButton
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        private Button createSaveButton(int tabIndex)
        {

            Button button = new Button();

            button.Location = new Point(270, 170);
            button.Name = "saveButton";
            button.Size = new Size(150, 23);
            button.TabIndex = tabIndex;
            button.Text = "入力した内容を保存する";
            button.UseVisualStyleBackColor = true;
            button.Click += saveButton_Click;

            return button;

        }

        private void saveButton_Click(object? sender, EventArgs e)
        {

            // 変更を通知する
            notify();

        }

        private void notify()
        {

            // 保存イベントのリスナーに声をかける
            foreach (ActionListener actionListener in actionListeners)
            {

                actionListener.listen(Event.Save);

            }

            // データの更新がかかったので、オブザーバーに更新を促す
            foreach (Observer observer in observers)
            {

                observer.update();

            }

        }

        public PersonPanel addObserver(Observer observer)
        {

            Debug.Assert(observer != null);

            observers.Add(observer);

            Debug.Assert(observers.Contains(observer));

            return this;

        }
        
        public PersonPanel addActionListener(ActionListener actionListener)
        {

            Debug.Assert(actionListener != null);

            actionListeners.Add(actionListener);

            Debug.Assert(actionListeners.Contains(actionListener));

            return this;

        }

    }

}
