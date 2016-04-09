using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFrmDemo
{
    

    public partial class MainForm : Form
    {
        #region 方法1(不推荐)--通过保存对象的引用调用的对象的公有方法实现窗体的传值
        //当接收数据的窗体增加，需要修改发送消息的代码，并且增加相应数量的窗体引用  可扩展性差，耦合性较高
        //public ObeserverFormA ChildFormA { get; set; }
        //public ObeserverFormB ChildFormB { get; set; } 
        #endregion

        #region 方法2---委托方式传值
        //定义发布消息的委托  委托是一个类型 委托可以在外部获得执行
        public Action<string> SendMsg { get; set; } 
        #endregion

        #region 方法3（推荐）--事件方式
        //增加event关键字
        //定 义消息发布的事件  事件是委托的一个特殊实例  事件只能在类的内部触发执行
        public event EventHandler SendMsgEvent; //使用默认的事件处理委托
        #endregion


       
        public MainForm()
        {
            InitializeComponent();
        }

        private void ParentFrm_Load(object sender, EventArgs e)
        {

            #region 方法1(不推荐)
            //ObeserverFormA childFormA = new ObeserverFormA();
            //ChildFormA = childFormA;
            //childFormA.Show();
            //ObeserverFormB childFormB = new ObeserverFormB();
            //ChildFormB = childFormB;
            //childFormB.Show(); 
            #endregion

            #region 方法2---委托方式传值
            //子窗体弹出来之前，为委托赋值，关注主窗体消息的变化，当有多个窗体需要接收信息，只需要在此修改即可
            //ObeserverFormA childFormA = new ObeserverFormA();
            //SendMsg += childFormA.SetText;//委托赋值
            //childFormA.Show();
            //ObeserverFormB childFormB = new ObeserverFormB();
            //SendMsg += childFormB.SetText;
            //childFormB.Show(); 
            #endregion


            #region 方法3（推荐）--事件方式
            //子窗体弹出来之前，注册事件，关注主窗体消息的变化，当有多个窗体需要接收信息，只需要在此修改即可
            ObeserverFormA childFormA = new ObeserverFormA();
            SendMsgEvent += childFormA.MainFormTxtChaned;//为子窗体注册事件，在子窗体中事件处理代码中设置文本
            childFormA.Show();
            ObeserverFormB childFormB = new ObeserverFormB();
            SendMsgEvent += childFormB.MainFormTxtChaned;
            childFormB.Show();
            #endregion


            
        }

        //当MainForm中输入文本，点击发送消息，子窗体的文本框显示主窗体的数据
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            #region 方法1(不推荐)

            //ChildFormA.SetText(this.txtMsg.Text);
            //ChildFormB.SetText(this.txtMsg.Text); 

            #endregion

            #region 方法2---委托方式传值
            //if (SendMsg!=null)
            //{
            //    SendMsg(this.txtMsg.Text);//执行所有注册的委托
            //}

            #endregion

            #region 方法3（推荐）--事件方式
            //触发事件
            //EventArgs,写一个子类继承该类，子类中添加需要封装的数据信息，此处只需要传递string信息，详见MyEventArgs
            SendMsgEvent(this,new MyEventArg(){Text=this.txtMsg.Text});
            #endregion
        }
    }
}
