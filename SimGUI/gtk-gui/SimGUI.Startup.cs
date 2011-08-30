
// This file has been generated by the GUI designer. Do not modify.
namespace SimGUI {
	public partial class Startup {
		private global::Gtk.UIManager UIManager;

		private global::Gtk.VBox vbox2;

		private global::Gtk.MenuBar menubar1;

		private global::Gtk.Table table1;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Entry entry2;

		private global::Gtk.SpinButton spinbutton7;

		private global::Gtk.Label label1;

		private global::Gtk.Label label10;

		private global::Gtk.Label label6;

		private global::Gtk.Label label8;

		private global::Gtk.Label label9;

		private global::Gtk.SpinButton spinbutton1;

		private global::Gtk.SpinButton spinbutton2;

		private global::Gtk.SpinButton spinbutton6;

		private global::Gtk.SpinButton spinbutton8;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Button button2;

		private global::Gtk.Button button3;

		protected virtual void Build() {
			global::Stetic.Gui.Initialize(this);
			// Widget SimGUI.Startup
			this.UIManager = new global::Gtk.UIManager();
			global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup("Default");
			this.UIManager.InsertActionGroup(w1, 0);
			this.AddAccelGroup(this.UIManager.AccelGroup);
			this.Name = "SimGUI.Startup";
			this.Title = global::Mono.Unix.Catalog.GetString("Startup");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child SimGUI.Startup.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString("<ui><menubar name='menubar1'><menu><menuitem/></menu><menu><menuitem/><menuitem/><menuitem/></menu></menubar></ui>");
			this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget("/menubar1")));
			this.menubar1.Name = "menubar1";
			this.vbox2.Add(this.menubar1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.menubar1]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(5)), ((uint)(2)), false);
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.entry2 = new global::Gtk.Entry();
			this.entry2.CanFocus = true;
			this.entry2.Name = "entry2";
			this.entry2.Text = global::Mono.Unix.Catalog.GetString("D");
			this.entry2.IsEditable = true;
			this.entry2.MaxLength = 1;
			this.entry2.InvisibleChar = '●';
			this.hbox3.Add(this.entry2);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.entry2]));
			w3.Position = 0;
			// Container child hbox3.Gtk.Box+BoxChild
			this.spinbutton7 = new global::Gtk.SpinButton(0, 228, 1);
			this.spinbutton7.CanFocus = true;
			this.spinbutton7.Name = "spinbutton7";
			this.spinbutton7.Adjustment.PageIncrement = 10;
			this.spinbutton7.ClimbRate = 1;
			this.spinbutton7.Numeric = true;
			this.spinbutton7.Value = 228;
			this.hbox3.Add(this.spinbutton7);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.spinbutton7]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.table1.Add(this.hbox3);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox3]));
			w5.TopAttach = ((uint)(4));
			w5.BottomAttach = ((uint)(5));
			w5.LeftAttach = ((uint)(1));
			w5.RightAttach = ((uint)(2));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Neutron flux");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w6.TopAttach = ((uint)(3));
			w6.BottomAttach = ((uint)(4));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label10 = new global::Gtk.Label();
			this.label10.Name = "label10";
			this.label10.LabelProp = global::Mono.Unix.Catalog.GetString("Rod position");
			this.table1.Add(this.label10);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.label10]));
			w7.TopAttach = ((uint)(4));
			w7.BottomAttach = ((uint)(5));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label6 = new global::Gtk.Label();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString("Turbine power");
			this.table1.Add(this.label6);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.label6]));
			w8.TopAttach = ((uint)(2));
			w8.BottomAttach = ((uint)(3));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label8 = new global::Gtk.Label();
			this.label8.Name = "label8";
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString("Boron concentration");
			this.table1.Add(this.label8);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.label8]));
			w9.TopAttach = ((uint)(1));
			w9.BottomAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label9 = new global::Gtk.Label();
			this.label9.Name = "label9";
			this.label9.LabelProp = global::Mono.Unix.Catalog.GetString("Burnup");
			this.table1.Add(this.label9);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.label9]));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinbutton1 = new global::Gtk.SpinButton(0, 30000, 1);
			this.spinbutton1.CanFocus = true;
			this.spinbutton1.Name = "spinbutton1";
			this.spinbutton1.Adjustment.PageIncrement = 10;
			this.spinbutton1.ClimbRate = 1;
			this.spinbutton1.Numeric = true;
			this.spinbutton1.Value = 150;
			this.table1.Add(this.spinbutton1);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.spinbutton1]));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinbutton2 = new global::Gtk.SpinButton(0, 5000, 1);
			this.spinbutton2.CanFocus = true;
			this.spinbutton2.Name = "spinbutton2";
			this.spinbutton2.Adjustment.PageIncrement = 10;
			this.spinbutton2.ClimbRate = 1;
			this.spinbutton2.Numeric = true;
			this.spinbutton2.Value = 1711;
			this.table1.Add(this.spinbutton2);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1[this.spinbutton2]));
			w12.TopAttach = ((uint)(1));
			w12.BottomAttach = ((uint)(2));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinbutton6 = new global::Gtk.SpinButton(0, 200, 1);
			this.spinbutton6.CanFocus = true;
			this.spinbutton6.Name = "spinbutton6";
			this.spinbutton6.Adjustment.PageIncrement = 10;
			this.spinbutton6.ClimbRate = 1;
			this.spinbutton6.Numeric = true;
			this.spinbutton6.Value = 100;
			this.table1.Add(this.spinbutton6);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1[this.spinbutton6]));
			w13.TopAttach = ((uint)(2));
			w13.BottomAttach = ((uint)(3));
			w13.LeftAttach = ((uint)(1));
			w13.RightAttach = ((uint)(2));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinbutton8 = new global::Gtk.SpinButton(0, 200, 1);
			this.spinbutton8.CanFocus = true;
			this.spinbutton8.Name = "spinbutton8";
			this.spinbutton8.Adjustment.PageIncrement = 10;
			this.spinbutton8.ClimbRate = 1;
			this.spinbutton8.Numeric = true;
			this.spinbutton8.Value = 100;
			this.table1.Add(this.spinbutton8);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1[this.spinbutton8]));
			w14.TopAttach = ((uint)(3));
			w14.BottomAttach = ((uint)(4));
			w14.LeftAttach = ((uint)(1));
			w14.RightAttach = ((uint)(2));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox2.Add(this.table1);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.table1]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.button2 = new global::Gtk.Button();
			this.button2.CanFocus = true;
			this.button2.Name = "button2";
			this.button2.UseUnderline = true;
			this.button2.Label = global::Mono.Unix.Catalog.GetString("Set snapshot");
			this.hbox1.Add(this.button2);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.button2]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.button3 = new global::Gtk.Button();
			this.button3.CanFocus = true;
			this.button3.Name = "button3";
			this.button3.UseUnderline = true;
			this.button3.Label = global::Mono.Unix.Catalog.GetString("Load snapshot");
			this.hbox1.Add(this.button3);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.button3]));
			w17.Position = 2;
			w17.Expand = false;
			w17.Fill = false;
			this.vbox2.Add(this.hbox1);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
			w18.Position = 3;
			w18.Expand = false;
			w18.Fill = false;
			this.Add(this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll();
			}
			this.DefaultWidth = 411;
			this.DefaultHeight = 197;
			this.Show();
			this.button2.Clicked += new global::System.EventHandler(this.OnButton2Clicked);
		}
	}
}
