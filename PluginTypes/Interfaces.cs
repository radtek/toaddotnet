using System.Windows.Forms;

namespace PluginTypes
{
    public interface IFormAddOn
    {
        void Install(Form form);
    }
    public interface IMenuAddOn
    {
        void Install(MenuStrip menu);
    }

    public interface  ITabPageAddOn
    {
        void Install(TabControl tabControl);
        void EventPlug(PlugEvent e);
    }

    public interface ITabPageLeftAddOn
    {
        void Install(TabControl tabControl);
        void EventPlug(PlugEvent e);
    }

    public interface IGroupBoxAddOn
    {
        void Install(TabPage tabPage);
        void EventPlug(PlugEvent e);
    }
}
