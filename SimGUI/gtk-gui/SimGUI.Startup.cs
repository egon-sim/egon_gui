// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace SimGUI {
    
    
    public partial class Startup {
        
        private Gtk.UIManager UIManager;
        
        private Gtk.Action FIleAction;
        
        private Gtk.Action ExitAction;
        
        private Gtk.Action PanelsAction;
        
        private Gtk.Action ReactorAction;
        
        private Gtk.Action TurbineAction;
        
        private Gtk.Action TrendsAction;
        
        private Gtk.VBox vbox2;
        
        private Gtk.MenuBar menubar1;
        
        private Gtk.Table table1;
        
        private Gtk.Entry entry1;
        
        private Gtk.Entry entry3;
        
        private Gtk.HBox hbox3;
        
        private Gtk.Entry entry2;
        
        private Gtk.SpinButton spinbutton7;
        
        private Gtk.Label label1;
        
        private Gtk.Label label10;
        
        private Gtk.Label label2;
        
        private Gtk.Label label3;
        
        private Gtk.Label label4;
        
        private Gtk.Label label6;
        
        private Gtk.Label label7;
        
        private Gtk.Label label8;
        
        private Gtk.Label label9;
        
        private Gtk.SpinButton spinbutton1;
        
        private Gtk.SpinButton spinbutton2;
        
        private Gtk.SpinButton spinbutton3;
        
        private Gtk.SpinButton spinbutton4;
        
        private Gtk.SpinButton spinbutton5;
        
        private Gtk.SpinButton spinbutton6;
        
        private Gtk.Button button1;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget SimGUI.Startup
            this.UIManager = new Gtk.UIManager();
            Gtk.ActionGroup w1 = new Gtk.ActionGroup("Default");
            this.FIleAction = new Gtk.Action("FIleAction", Mono.Unix.Catalog.GetString("FIle"), null, null);
            this.FIleAction.ShortLabel = Mono.Unix.Catalog.GetString("FIle");
            w1.Add(this.FIleAction, null);
            this.ExitAction = new Gtk.Action("ExitAction", Mono.Unix.Catalog.GetString("Exit"), null, null);
            this.ExitAction.ShortLabel = Mono.Unix.Catalog.GetString("Exit");
            w1.Add(this.ExitAction, null);
            this.PanelsAction = new Gtk.Action("PanelsAction", Mono.Unix.Catalog.GetString("Panels"), null, null);
            this.PanelsAction.ShortLabel = Mono.Unix.Catalog.GetString("Panels");
            w1.Add(this.PanelsAction, null);
            this.ReactorAction = new Gtk.Action("ReactorAction", Mono.Unix.Catalog.GetString("Reactor"), null, null);
            this.ReactorAction.ShortLabel = Mono.Unix.Catalog.GetString("Reactor");
            w1.Add(this.ReactorAction, null);
            this.TurbineAction = new Gtk.Action("TurbineAction", Mono.Unix.Catalog.GetString("Turbine"), null, null);
            this.TurbineAction.ShortLabel = Mono.Unix.Catalog.GetString("Turbine");
            w1.Add(this.TurbineAction, null);
            this.TrendsAction = new Gtk.Action("TrendsAction", Mono.Unix.Catalog.GetString("Trends"), null, null);
            this.TrendsAction.ShortLabel = Mono.Unix.Catalog.GetString("Trends");
            w1.Add(this.TrendsAction, null);
            this.UIManager.InsertActionGroup(w1, 0);
            this.AddAccelGroup(this.UIManager.AccelGroup);
            this.Name = "SimGUI.Startup";
            this.Title = Mono.Unix.Catalog.GetString("Startup");
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            // Container child SimGUI.Startup.Gtk.Container+ContainerChild
            this.vbox2 = new Gtk.VBox();
            this.vbox2.Name = "vbox2";
            this.vbox2.Spacing = 6;
            // Container child vbox2.Gtk.Box+BoxChild
            this.UIManager.AddUiFromString("<ui><menubar name='menubar1'><menu name='FIleAction' action='FIleAction'><menuitem name='ExitAction' action='ExitAction'/></menu><menu name='PanelsAction' action='PanelsAction'><menuitem name='ReactorAction' action='ReactorAction'/><menuitem name='TurbineAction' action='TurbineAction'/><menuitem name='TrendsAction' action='TrendsAction'/></menu></menubar></ui>");
            this.menubar1 = ((Gtk.MenuBar)(this.UIManager.GetWidget("/menubar1")));
            this.menubar1.Name = "menubar1";
            this.vbox2.Add(this.menubar1);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.vbox2[this.menubar1]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.table1 = new Gtk.Table(((uint)(9)), ((uint)(2)), false);
            this.table1.Name = "table1";
            this.table1.RowSpacing = ((uint)(6));
            this.table1.ColumnSpacing = ((uint)(6));
            // Container child table1.Gtk.Table+TableChild
            this.entry1 = new Gtk.Entry();
            this.entry1.CanFocus = true;
            this.entry1.Name = "entry1";
            this.entry1.Text = Mono.Unix.Catalog.GetString("reactor");
            this.entry1.IsEditable = true;
            this.entry1.InvisibleChar = '●';
            this.table1.Add(this.entry1);
            Gtk.Table.TableChild w3 = ((Gtk.Table.TableChild)(this.table1[this.entry1]));
            w3.LeftAttach = ((uint)(1));
            w3.RightAttach = ((uint)(2));
            w3.XOptions = ((Gtk.AttachOptions)(4));
            w3.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.entry3 = new Gtk.Entry();
            this.entry3.CanFocus = true;
            this.entry3.Name = "entry3";
            this.entry3.Text = Mono.Unix.Catalog.GetString("miljenko");
            this.entry3.IsEditable = true;
            this.entry3.InvisibleChar = '●';
            this.table1.Add(this.entry3);
            Gtk.Table.TableChild w4 = ((Gtk.Table.TableChild)(this.table1[this.entry3]));
            w4.TopAttach = ((uint)(1));
            w4.BottomAttach = ((uint)(2));
            w4.LeftAttach = ((uint)(1));
            w4.RightAttach = ((uint)(2));
            w4.XOptions = ((Gtk.AttachOptions)(4));
            w4.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.hbox3 = new Gtk.HBox();
            this.hbox3.Name = "hbox3";
            this.hbox3.Spacing = 6;
            // Container child hbox3.Gtk.Box+BoxChild
            this.entry2 = new Gtk.Entry();
            this.entry2.CanFocus = true;
            this.entry2.Name = "entry2";
            this.entry2.Text = Mono.Unix.Catalog.GetString("D");
            this.entry2.IsEditable = true;
            this.entry2.MaxLength = 1;
            this.entry2.InvisibleChar = '●';
            this.hbox3.Add(this.entry2);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.hbox3[this.entry2]));
            w5.Position = 0;
            // Container child hbox3.Gtk.Box+BoxChild
            this.spinbutton7 = new Gtk.SpinButton(0, 228, 1);
            this.spinbutton7.CanFocus = true;
            this.spinbutton7.Name = "spinbutton7";
            this.spinbutton7.Adjustment.PageIncrement = 10;
            this.spinbutton7.ClimbRate = 1;
            this.spinbutton7.Numeric = true;
            this.spinbutton7.Value = 228;
            this.hbox3.Add(this.spinbutton7);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.hbox3[this.spinbutton7]));
            w6.Position = 1;
            w6.Expand = false;
            w6.Fill = false;
            this.table1.Add(this.hbox3);
            Gtk.Table.TableChild w7 = ((Gtk.Table.TableChild)(this.table1[this.hbox3]));
            w7.TopAttach = ((uint)(8));
            w7.BottomAttach = ((uint)(9));
            w7.LeftAttach = ((uint)(1));
            w7.RightAttach = ((uint)(2));
            w7.XOptions = ((Gtk.AttachOptions)(4));
            w7.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("Server name");
            this.table1.Add(this.label1);
            Gtk.Table.TableChild w8 = ((Gtk.Table.TableChild)(this.table1[this.label1]));
            w8.XOptions = ((Gtk.AttachOptions)(4));
            w8.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label10 = new Gtk.Label();
            this.label10.Name = "label10";
            this.label10.LabelProp = Mono.Unix.Catalog.GetString("Rod position");
            this.table1.Add(this.label10);
            Gtk.Table.TableChild w9 = ((Gtk.Table.TableChild)(this.table1[this.label10]));
            w9.TopAttach = ((uint)(8));
            w9.BottomAttach = ((uint)(9));
            w9.XOptions = ((Gtk.AttachOptions)(4));
            w9.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.LabelProp = Mono.Unix.Catalog.GetString("Tavg");
            this.table1.Add(this.label2);
            Gtk.Table.TableChild w10 = ((Gtk.Table.TableChild)(this.table1[this.label2]));
            w10.TopAttach = ((uint)(4));
            w10.BottomAttach = ((uint)(5));
            w10.XOptions = ((Gtk.AttachOptions)(4));
            w10.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label3 = new Gtk.Label();
            this.label3.Name = "label3";
            this.label3.LabelProp = Mono.Unix.Catalog.GetString("Host name");
            this.table1.Add(this.label3);
            Gtk.Table.TableChild w11 = ((Gtk.Table.TableChild)(this.table1[this.label3]));
            w11.TopAttach = ((uint)(1));
            w11.BottomAttach = ((uint)(2));
            w11.XOptions = ((Gtk.AttachOptions)(4));
            w11.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label4 = new Gtk.Label();
            this.label4.Name = "label4";
            this.label4.LabelProp = Mono.Unix.Catalog.GetString("Tref");
            this.table1.Add(this.label4);
            Gtk.Table.TableChild w12 = ((Gtk.Table.TableChild)(this.table1[this.label4]));
            w12.TopAttach = ((uint)(5));
            w12.BottomAttach = ((uint)(6));
            w12.XOptions = ((Gtk.AttachOptions)(4));
            w12.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label6 = new Gtk.Label();
            this.label6.Name = "label6";
            this.label6.LabelProp = Mono.Unix.Catalog.GetString("Turbine power");
            this.table1.Add(this.label6);
            Gtk.Table.TableChild w13 = ((Gtk.Table.TableChild)(this.table1[this.label6]));
            w13.TopAttach = ((uint)(7));
            w13.BottomAttach = ((uint)(8));
            w13.XOptions = ((Gtk.AttachOptions)(4));
            w13.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label7 = new Gtk.Label();
            this.label7.Name = "label7";
            this.label7.LabelProp = Mono.Unix.Catalog.GetString("Neutron flux");
            this.table1.Add(this.label7);
            Gtk.Table.TableChild w14 = ((Gtk.Table.TableChild)(this.table1[this.label7]));
            w14.TopAttach = ((uint)(6));
            w14.BottomAttach = ((uint)(7));
            w14.XOptions = ((Gtk.AttachOptions)(4));
            w14.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label8 = new Gtk.Label();
            this.label8.Name = "label8";
            this.label8.LabelProp = Mono.Unix.Catalog.GetString("Boron concentration");
            this.table1.Add(this.label8);
            Gtk.Table.TableChild w15 = ((Gtk.Table.TableChild)(this.table1[this.label8]));
            w15.TopAttach = ((uint)(3));
            w15.BottomAttach = ((uint)(4));
            w15.XOptions = ((Gtk.AttachOptions)(4));
            w15.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.label9 = new Gtk.Label();
            this.label9.Name = "label9";
            this.label9.LabelProp = Mono.Unix.Catalog.GetString("Burnup");
            this.table1.Add(this.label9);
            Gtk.Table.TableChild w16 = ((Gtk.Table.TableChild)(this.table1[this.label9]));
            w16.TopAttach = ((uint)(2));
            w16.BottomAttach = ((uint)(3));
            w16.XOptions = ((Gtk.AttachOptions)(4));
            w16.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.spinbutton1 = new Gtk.SpinButton(0, 30000, 1);
            this.spinbutton1.CanFocus = true;
            this.spinbutton1.Name = "spinbutton1";
            this.spinbutton1.Adjustment.PageIncrement = 10;
            this.spinbutton1.ClimbRate = 1;
            this.spinbutton1.Numeric = true;
            this.spinbutton1.Value = 150;
            this.table1.Add(this.spinbutton1);
            Gtk.Table.TableChild w17 = ((Gtk.Table.TableChild)(this.table1[this.spinbutton1]));
            w17.TopAttach = ((uint)(2));
            w17.BottomAttach = ((uint)(3));
            w17.LeftAttach = ((uint)(1));
            w17.RightAttach = ((uint)(2));
            w17.XOptions = ((Gtk.AttachOptions)(4));
            w17.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.spinbutton2 = new Gtk.SpinButton(0, 5000, 1);
            this.spinbutton2.CanFocus = true;
            this.spinbutton2.Name = "spinbutton2";
            this.spinbutton2.Adjustment.PageIncrement = 10;
            this.spinbutton2.ClimbRate = 1;
            this.spinbutton2.Numeric = true;
            this.spinbutton2.Value = 1711;
            this.table1.Add(this.spinbutton2);
            Gtk.Table.TableChild w18 = ((Gtk.Table.TableChild)(this.table1[this.spinbutton2]));
            w18.TopAttach = ((uint)(3));
            w18.BottomAttach = ((uint)(4));
            w18.LeftAttach = ((uint)(1));
            w18.RightAttach = ((uint)(2));
            w18.XOptions = ((Gtk.AttachOptions)(4));
            w18.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.spinbutton3 = new Gtk.SpinButton(0, 350, 1);
            this.spinbutton3.CanFocus = true;
            this.spinbutton3.Name = "spinbutton3";
            this.spinbutton3.Adjustment.PageIncrement = 10;
            this.spinbutton3.ClimbRate = 1;
            this.spinbutton3.Numeric = true;
            this.spinbutton3.Value = 305;
            this.table1.Add(this.spinbutton3);
            Gtk.Table.TableChild w19 = ((Gtk.Table.TableChild)(this.table1[this.spinbutton3]));
            w19.TopAttach = ((uint)(4));
            w19.BottomAttach = ((uint)(5));
            w19.LeftAttach = ((uint)(1));
            w19.RightAttach = ((uint)(2));
            w19.XOptions = ((Gtk.AttachOptions)(4));
            w19.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.spinbutton4 = new Gtk.SpinButton(0, 350, 1);
            this.spinbutton4.CanFocus = true;
            this.spinbutton4.Name = "spinbutton4";
            this.spinbutton4.Adjustment.PageIncrement = 10;
            this.spinbutton4.ClimbRate = 1;
            this.spinbutton4.Numeric = true;
            this.spinbutton4.Value = 305;
            this.table1.Add(this.spinbutton4);
            Gtk.Table.TableChild w20 = ((Gtk.Table.TableChild)(this.table1[this.spinbutton4]));
            w20.TopAttach = ((uint)(5));
            w20.BottomAttach = ((uint)(6));
            w20.LeftAttach = ((uint)(1));
            w20.RightAttach = ((uint)(2));
            w20.XOptions = ((Gtk.AttachOptions)(4));
            w20.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.spinbutton5 = new Gtk.SpinButton(0, 200, 1);
            this.spinbutton5.CanFocus = true;
            this.spinbutton5.Name = "spinbutton5";
            this.spinbutton5.Adjustment.PageIncrement = 10;
            this.spinbutton5.ClimbRate = 1;
            this.spinbutton5.Numeric = true;
            this.spinbutton5.Value = 100;
            this.table1.Add(this.spinbutton5);
            Gtk.Table.TableChild w21 = ((Gtk.Table.TableChild)(this.table1[this.spinbutton5]));
            w21.TopAttach = ((uint)(6));
            w21.BottomAttach = ((uint)(7));
            w21.LeftAttach = ((uint)(1));
            w21.RightAttach = ((uint)(2));
            w21.XOptions = ((Gtk.AttachOptions)(4));
            w21.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table1.Gtk.Table+TableChild
            this.spinbutton6 = new Gtk.SpinButton(0, 200, 1);
            this.spinbutton6.CanFocus = true;
            this.spinbutton6.Name = "spinbutton6";
            this.spinbutton6.Adjustment.PageIncrement = 10;
            this.spinbutton6.ClimbRate = 1;
            this.spinbutton6.Numeric = true;
            this.table1.Add(this.spinbutton6);
            Gtk.Table.TableChild w22 = ((Gtk.Table.TableChild)(this.table1[this.spinbutton6]));
            w22.TopAttach = ((uint)(7));
            w22.BottomAttach = ((uint)(8));
            w22.LeftAttach = ((uint)(1));
            w22.RightAttach = ((uint)(2));
            w22.XOptions = ((Gtk.AttachOptions)(4));
            w22.YOptions = ((Gtk.AttachOptions)(4));
            this.vbox2.Add(this.table1);
            Gtk.Box.BoxChild w23 = ((Gtk.Box.BoxChild)(this.vbox2[this.table1]));
            w23.Position = 1;
            w23.Expand = false;
            w23.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.button1 = new Gtk.Button();
            this.button1.CanFocus = true;
            this.button1.Name = "button1";
            this.button1.UseUnderline = true;
            this.button1.Label = Mono.Unix.Catalog.GetString("Start simulator");
            this.vbox2.Add(this.button1);
            Gtk.Box.BoxChild w24 = ((Gtk.Box.BoxChild)(this.vbox2[this.button1]));
            w24.Position = 2;
            w24.Expand = false;
            w24.Fill = false;
            this.Add(this.vbox2);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 380;
            this.DefaultHeight = 372;
            this.Show();
            this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
            this.ExitAction.Activated += new System.EventHandler(this.OnExitActionActivated);
            this.ReactorAction.Activated += new System.EventHandler(this.OnReactorActionActivated);
            this.TurbineAction.Activated += new System.EventHandler(this.OnTurbineActionActivated);
            this.button1.Clicked += new System.EventHandler(this.OnButton1Clicked);
        }
    }
}
