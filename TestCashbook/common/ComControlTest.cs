using cashbook.common;

namespace TestCashbook.common
{
    [TestClass]
    public class ComCotrolTest
    {
        [TestMethod]
        public void TestSetErrorLabelColor()
        {
            // ���Ғl�ݒ�
            object e = Color.Red;

            // �p�����[�^�ݒ�
            Label label = new();

            // �������s
            ComControl.SetErrorLabelColor(label);

            // �����l�ݒ�
            object a = label.BackColor;

            Assert.AreEqual(e,a);
        }
    }
}