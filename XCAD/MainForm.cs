﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TKBO;
using TKBRep;
using TKernel;
using TKFillet;
using TKMath;
using TKPrim;
using TKTopAlgo;
using TKV3d;
using TKG3d;
using TKGeomBase;
using TKLCAF;
using TKVCAF;
using TKXCAF;
using TKXDESTEP;
using OCCT.Foundation.Net;
using XCAD.Modules;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using XModel.Common;
using XModel.Elements;
using XModel.Interface;
using System.Runtime.Loader;
using DevExpress.XtraEditors;
using TKService;

namespace XCAD
{
    public partial class MainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        #region 构型初始
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Initialize()) {
                Application.Exit();
                return;
            }
            InitLayoutMessage();
            LoadedPlugins();
            this.accordionControl.OptionsHamburgerMenu.DisplayMode = AccordionControlDisplayMode.Overlay;
            DisplayMessage(Guid.NewGuid().ToString(), "code 55", "工具准备就绪", "MainForm", 0);
            
            this.FormClosed += MainForm_FormClosed;
            this.accordionControl.ElementClick += AccordionControl_ElementClick;
            this.accordionControl.CustomDrawElement += accordionControl_CustomDrawElement;
            this.accordionControl.Refresh();
            this.Refresh();
        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (InitViewer) {
                OCCTView?.InitOCCTProxy();
                OCCTView = null;
                OCCTContext = null;
            }
        }


        /// <summary>
        /// 自动加载插件
        /// </summary>
        public void LoadedPlugins() {
            string PluginPath = @$"{Application.StartupPath}\plugins\";
            string[] FileInfos = Directory.GetFiles(PluginPath,"*.dll");
            foreach (string fileName in FileInfos) {
                XPluginAssembly plugin = new XPluginAssembly() {
                    PluginId = Guid.NewGuid().ToString(),
                    //AssemblyFullName = "XModelPlugin.ModelPlugin",
                    //AssemblyName = "ModelPlugin",
                    //    index = 0,
                    PluginCaption = Path.GetFileNameWithoutExtension(fileName),
                    PluginGroup = Path.GetFileNameWithoutExtension(fileName),
                    PluginName = Path.GetFileNameWithoutExtension(fileName),
                    PluginPath = @$"plugins\{Path.GetFileName(fileName)}"
                };
                XDesignPlugin DesignPlugin = new XDesignPlugin();
                XAssemblyLoadContext loadContext = XAssemblyLoadContext.AssemblyLoad(plugin, ref DesignPlugin);
                AccordionControlElement PluginNode = this.accordionElementPlugin;
                PluginNode = new AccordionControlElement();
                PluginNode.Name = $"{DesignPlugin.PluginName}";
                PluginNode.Text = $"{DesignPlugin.PluginCaption}";
                PluginNode.Tag = DesignPlugin;
                this.accordionElementPlugin.Elements.Add(PluginNode);
            }
            //XPluginAssembly plugin = new XPluginAssembly() {
            //    PluginId = Guid.NewGuid().ToString(),
            //    AssemblyFullName = "XModelPlugin.ModelPlugin",
            //    AssemblyName = "ModelPlugin",
            //    //    index = 0,
            //    PluginCaption = "Model",
            //    PluginGroup = "测试",
            //    PluginName = "test",
            //    PluginPath = @"plugins\XModelPlugin.dll"
            //};
            //XDesignPlugin DesignPlugin = new XDesignPlugin();
            //XAssemblyLoadContext context = new XAssemblyLoadContext();
            //AssemblyLoadContext loadContext = context.AssemblyLoad(plugin, ref DesignPlugin);
            //object result = DesignPlugin.Invoke("XModelPlugin.ModelPlugin", "Add", 10, 50);
            //object result1 = DesignPlugin.Invoke("Add", 10, 50);
            //XAssemblyLoadContext.UnLoadPlugin(DesignPlugin);
            //object result2 = DesignPlugin.Invoke("XModelPlugin.ModelPlugin", "Add", 10, 50);
            //object result3 = DesignPlugin.Invoke("Add", 10, 50);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            OCCTView = new OCCTProxy();
            OCCTView.InitOCCTProxy();
            if (!(InitViewer = OCCTView.InitViewer(this.RWControl.Handle))) {
                //DisplayMessage(Guid.NewGuid().ToString(), "code 98", "初始化图形失败", "MainForm", 3);
                return InitViewer;
            }
            myCurrentMode = CurrentAction3d.CurAction3d_DynamicRotation;
            myCurrentPressedKey = CurrentPressedKey.CurPressedKey_Nothing;
            myDegenerateModeIsOn = true;
            IsRectVisible = false;
            OCCTView.SetLight(true);
            OCCTView.SetSelectionStyle(XAspect_TypeOfHighlightMethod.Aspect_TOHM_COLOR, new XQuantity_Color(Color.Blue.R / 255, Color.Blue.G / 255, Color.Blue.B / 255, XQuantity_TypeOfColor.Quantity_TOC_RGB), 0, 1.0f);
            OCCTContext = OCCTView.GetInteractiveContext();
            this.RWControl.SizeChanged += RenderWindow_SizeChanged;
            this.RWControl.Paint += RenderWindow_Paint;
            this.RWControl.KeyDown += RenderWindow_KeyDown;
            this.RWControl.KeyUp += RenderWindow_KeyUp;
            this.RWControl.MouseDown += RenderWindow_MouseDown;
            this.RWControl.MouseUp += RenderWindow_MouseUp;
            this.RWControl.MouseMove += RenderWindow_MouseMove;
            this.RWControl.MouseWheel += RenderWindow_MouseWheel;
            return InitViewer;
        }
        private void accordionControl_CustomDrawElement(object sender, CustomDrawElementEventArgs e)
        {
            if (this.accordionControl.SelectedElement != null && this.accordionControl.SelectedElement.Elements.Count == 0 && e.ObjectInfo.Element == this.accordionControl.SelectedElement) {
                e.Handled = true;
                e.DrawHeaderBackground();
                e.DrawText();
                e.Cache.FillRectangle(e.ObjectInfo.PaintAppearance.ForeColor, new Rectangle(e.ObjectInfo.HeaderBounds.Location, new Size(ScaleHelper.ScaleHorizontal(3), e.ObjectInfo.HeaderBounds.Height)));
                e.ObjectInfo.Element.ImageOptions.Image = this.accordionControl.SelectedElement.ImageOptions.Image;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.flyoutMessagePanel.Options.VertIndent = this.RWControl.Height - 30;
            this.flyoutMessagePanel.Options.HorzIndent = (this.RWControl.Width - 350) / 2;
            //if (!IsHandleCreated || IsDisposed) return;
            //if (Width < ScaleHelper.ScaleHorizontal(850)) {
            //    accordionControl.OptionsHamburgerMenu.DisplayMode = AccordionControlDisplayMode.Overlay;
            //}
            //else {
            //    accordionControl.OptionsHamburgerMenu.DisplayMode = AccordionControlDisplayMode.Inline;
            //}
        }
        #endregion

        #region 元素事件
        /// <summary>
        /// 操作事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccordionControl_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            if (e.Element.Style == DevExpress.XtraBars.Navigation.ElementStyle.Group)
                return;
            switch (e.Element.Tag?.ToString()) {
                case "Open":
                    #region 打开文件
                    OperationOpenFile();
                    #endregion
                    break;
                case "Save":
                    #region 打开文件
                    OperationSaveFile();
                    #endregion
                    break;
                case "None":                            //特性 - 无
                case "Material":                        //特性 - 材料
                case "Texture":                         //特性 - 纹理
                    SetProperty(e.Element.Tag?.ToString());
                    break;
                case "HollowMesh":                      //显示模式 - 空心网格
                case "MeshEdges":                       //显示模式 - 网格边缘
                case "MeshShrinked":                    //显示模式 - 网格收缩
                case "Wireframe":                       //显示模式 - 线框模式
                case "FlatShaded":                      //显示模式 - 平面投影
                case "SmoothlyShaded":                  //显示模式 - 平滑阴影
                case "ShadedwithEdges":                 //显示模式 - 显示边框
                    SetViewDisplayMode(e.Element.Tag?.ToString());
                    break;
                case "Rubberbandselection":             //视图模式 - 橡皮筋选择
                case "Orbitrotationbysingletouch":      //视图模式 - 单触旋转
                case "Panbysingletouch":                //视图模式 - 单触平移
                case "Zoombysingletouch":               //视图模式 - 单触缩放
                case "ShowAll":                         //视图模式 - 显示所有
                case "FitAll":                          //视图模式 - 适合所有
                case "Enable/disableperspectivemodel":  //视图模式 - 透视模式
                case "Show/hidemessagewindow":          //视图模式 - 消息窗口
                case "Showselected":                    //视图模式 - 显示选择
                case "HideSelected":                    //视图模式 - 隐藏选择
                case "Showonlyselected":                //视图模式 - 仅显示选择
                case "PropertyWindow":                  //视图模式 - 属性窗口
                case "SelectParent":                    //视图模式 - 选择父级
                case "Assignorchangematerial":          //视图模式 - 指定材质
                case "DeleteSelected":                  //视图模式 - 删除选择
                    SetViewMode(e.Element.Tag?.ToString());
                    break;
                default:
                    break;
            }
        }

        #region 视图模式
        private void SetViewMode(string Tag, XAIS_InteractiveObject shape = null)
        {
            OCCTContext = OCCTView.GetInteractiveContext();
            switch (Tag) {
                case "Rubberbandselection":             //视图模式 - 橡皮筋选择
                    myCurrentMode = CurrentAction3d.CurAction3d_Nothing; 
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 橡皮筋选择", "MainForm", 0);
                    break;
                case "Orbitrotationbysingletouch":      //视图模式 - 单触旋转
                    myCurrentMode = CurrentAction3d.CurAction3d_DynamicRotation;
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 单触旋转", "MainForm", 0);
                    break;
                case "Panbysingletouch":                //视图模式 - 单触平移
                    myCurrentMode = CurrentAction3d.CurAction3d_DynamicPanning;
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 单触平移", "MainForm", 0);
                    break;
                case "Zoombysingletouch":               //视图模式 - 单触缩放
                    myCurrentMode = CurrentAction3d.CurAction3d_DynamicZooming;
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 单触缩放", "MainForm", 0);
                    break;
                case "ShowAll":                         //视图模式 - 显示所有
                    OCCTView.DisplayAll(true);
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 显示所有", "MainForm", 0);
                    break;
                case "FitAll":                          //视图模式 - 适合所有
                    OCCTView.ZoomAllView();
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 适合所有", "MainForm", 0);
                    break;
                case "Enable/disableperspectivemodel":  //视图模式 - 透视模式
                    XV3d_TypeOfView typeOfView = OCCTContext.CurrentViewer().DefaultTypeOfView();
                    OCCTContext.CurrentViewer().SetDefaultTypeOfView(typeOfView == XV3d_TypeOfView.V3d_ORTHOGRAPHIC ? XV3d_TypeOfView.V3d_PERSPECTIVE : XV3d_TypeOfView.V3d_ORTHOGRAPHIC);
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 透视模式", "MainForm", 0);
                    break;
                case "Showselected":                    //视图模式 - 显示选择
                    OCCTView.DisplaySelected(true);
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 显示选择", "MainForm", 0);
                    break;
                case "HideSelected":                    //视图模式 - 隐藏选择
                    OCCTView.EraseSelected(true);
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 隐藏选择", "MainForm", 0);
                    break;
                case "Showonlyselected":                //视图模式 - 仅显示选择
                    OCCTView.EraseAll(false);
                    OCCTView.DisplaySelected(true);
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 仅显示选择", "MainForm", 0);
                    break;
                case "SelectParent":                    //视图模式 - 选择父级
                    if (!OCCTView.IsObjectSelected())
                        break;
                    XAIS_InteractiveObject XObject = OCCTContext.SelectedInteractive();
                    XPrsMgr_PresentableObject xPObject = XObject.Parent();
                    XAIS_InteractiveObject PObject = OCCTView.DownCastObject(xPObject);
                    OCCTContext.AddOrRemoveSelected(PObject, true);
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 选择父级", "MainForm", 0);
                    break;
               
                case "DeleteSelected":                  //视图模式 - 删除选择
                    OCCTContext.Remove(OCCTContext.SelectedInteractive(), true);
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 删除选择", "MainForm", 0);
                    break;
                case "Show/hidemessagewindow":          //视图模式 - 消息窗口
                    if (!this.flyoutMessagePanel.IsPopupOpen) {
                        this.flyoutMessagePanel.Options.VertIndent = this.RWControl.Height - 30;
                        this.flyoutMessagePanel.Options.HorzIndent = (this.RWControl.Width - 350) / 2;
                        this.flyoutMessagePanel.ShowPopup();
                    }
                    else
                        this.flyoutMessagePanel.HidePopup();
                    break;
                case "PropertyWindow":                  //视图模式 - 属性窗口
                    break;
                case "Assignorchangematerial":          //视图模式 - 指定材质
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"视图模式 - 指定材质", "MainForm", 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 显示模式

        private void SetViewDisplayMode(string Tag, XAIS_InteractiveObject shape = null)
        {
            OCCTContext = OCCTView.GetInteractiveContext();
            switch (Tag) {
                case "HollowMesh":                      //显示模式 - 空心网格
                    if (shape != null) {
                        XPrs3d_Drawer shapeDrawer = shape.Attributes();
                        XPrs3d_ShadingAspect ShadingAspect = shapeDrawer.ShadingAspect();
                        XGraphic3d_AspectFillArea3d AspectFillArea3d = shapeDrawer.BasicFillAreaAspect();
                        if (AspectFillArea3d.ToDrawEdges())
                            AspectFillArea3d.SetEdgeOn();
                        else
                            AspectFillArea3d.SetEdgeOff();
                    }
                    else if (OCCTView.IsObjectSelected() && OCCTContext.SelectedInteractive() is XAIS_InteractiveObject SelectedShape && SelectedShape != null) {
                        XPrs3d_Drawer shapeDrawer = SelectedShape.Attributes();
                        XPrs3d_ShadingAspect ShadingAspect = shapeDrawer.ShadingAspect();
                        XGraphic3d_AspectFillArea3d AspectFillArea3d = shapeDrawer.BasicFillAreaAspect();
                        if (AspectFillArea3d.ToDrawEdges())
                            AspectFillArea3d.SetEdgeOn();
                        else
                            AspectFillArea3d.SetEdgeOff();
                    }

                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"显示模式 - 空心网格", "MainForm", 0);
                    break;
                case "MeshEdges":                       //显示模式 - 网格边缘
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"显示模式 - 网格边缘", "MainForm", 0);
                    break;
                case "MeshShrinked":                    //显示模式 - 网格收缩

                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"显示模式 - 网格收缩", "MainForm", 0);
                    break;
                case "Wireframe":                       //显示模式 - 线框模式
                    int Mode = OCCTView.DisplayMode();
                    SetDisplayMode(Mode == 0 ? 1 : 0);
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"显示模式 - 线框模式", "MainForm", 0);
                    break;
                case "FlatShaded":                      //显示模式 - 平面投影

                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"显示模式 - 平面投影", "MainForm", 0);
                    break;
                case "SmoothlyShaded":                  //显示模式 - 平滑阴影

                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"显示模式 - 平滑阴影", "MainForm", 0);
                    break;
                case "ShadedwithEdges":                 //显示模式 - 显示边框
                    if (shape != null) {
                        XPrs3d_Drawer shapeDrawer = shape.Attributes();
                        XPrs3d_ShadingAspect ShadingAspect = shapeDrawer.ShadingAspect();
                        XGraphic3d_AspectFillArea3d AspectFillArea3d = shapeDrawer.BasicFillAreaAspect();
                        if (AspectFillArea3d.ToDrawEdges()) {
                            AspectFillArea3d.SetEdgeOff();
                            shapeDrawer.SetFaceBoundaryDraw(false);
                        }
                        else {
                            AspectFillArea3d.SetEdgeOn();
                            shapeDrawer.SetFaceBoundaryDraw(true);
                        }
                        UpdateCurrentViewer();
                    }
                    else if (OCCTView.IsObjectSelected() && OCCTContext.SelectedInteractive() is XAIS_InteractiveObject SelectedShape && SelectedShape != null) {
                        XPrs3d_Drawer shapeDrawer = SelectedShape.Attributes();
                        XPrs3d_ShadingAspect ShadingAspect = shapeDrawer.ShadingAspect();
                        XGraphic3d_AspectFillArea3d AspectFillArea3d = shapeDrawer.BasicFillAreaAspect();
                        if (AspectFillArea3d.ToDrawEdges()) {
                            AspectFillArea3d.SetEdgeOff();
                            shapeDrawer.SetFaceBoundaryDraw(false);
                        }
                        else {
                            AspectFillArea3d.SetEdgeOn();
                            shapeDrawer.SetFaceBoundaryDraw(true);
                        }
                        UpdateCurrentViewer();
                    }
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"显示模式 - 显示边框", "MainForm", 0);
                    break;
            }
        }
        #endregion

        #region 特性修改
        /// <summary>
        /// 设置指定或者选择的图形的展示特性
        /// </summary>
        /// <param name="Tag">展示方式</param>
        /// <param name="shape">指定的图形</param>
        /// <example>
        /// shape = null 且无选择的图形时，不做任何操作；
        /// shape = null 且有选择的图形时，修改选择的图形的特性；
        /// shape != null时，修改指定的shape的特性
        /// </example>
        private void SetProperty(string Tag, XAIS_InteractiveObject shape = null)
        {
            OCCTContext = OCCTView.GetInteractiveContext();
            switch (Tag) {
                case "Material":                        //特性 - 材料
                    if (shape != null) {
                        XPrs3d_Drawer shapeDrawer = shape.Attributes();
                        XPrs3d_ShadingAspect ShadingAspect = shapeDrawer.ShadingAspect();
                        XGraphic3d_AspectFillArea3d AspectFillArea3d = shapeDrawer.BasicFillAreaAspect();
                        AspectFillArea3d.SetDistinguishOn();
                        AspectFillArea3d.SetTextureMapOff();
                    }
                    else if (OCCTView.IsObjectSelected() && OCCTContext.SelectedInteractive() is XAIS_InteractiveObject SelectedShape && SelectedShape != null) {
                        XPrs3d_Drawer shapeDrawer = SelectedShape.Attributes();
                        XPrs3d_ShadingAspect ShadingAspect = shapeDrawer.ShadingAspect();
                        XGraphic3d_AspectFillArea3d AspectFillArea3d = shapeDrawer.BasicFillAreaAspect();
                        AspectFillArea3d.SetDistinguishOn();
                        AspectFillArea3d.SetTextureMapOff();
                    }
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"开启材料特性", "MainForm", 0);
                    break;
                case "Texture":                         //特性 - 纹理
                    if (shape != null) {
                        XPrs3d_Drawer shapeDrawer = shape.Attributes();
                        XPrs3d_ShadingAspect ShadingAspect = shapeDrawer.ShadingAspect();
                        XGraphic3d_AspectFillArea3d AspectFillArea3d = shapeDrawer.BasicFillAreaAspect();
                        AspectFillArea3d.SetDistinguishOff();
                        AspectFillArea3d.SetTextureMapOn();
                    }
                    else if (OCCTView.IsObjectSelected() && OCCTContext.SelectedInteractive() is XAIS_InteractiveObject SelectedShape && SelectedShape != null) {
                        XPrs3d_Drawer shapeDrawer = SelectedShape.Attributes();
                        XPrs3d_ShadingAspect ShadingAspect = shapeDrawer.ShadingAspect();
                        XGraphic3d_AspectFillArea3d AspectFillArea3d = shapeDrawer.BasicFillAreaAspect();
                        AspectFillArea3d.SetDistinguishOff();
                        AspectFillArea3d.SetTextureMapOn();
                    }
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"开启纹理特性", "MainForm", 0);
                    break;
                case "None":                            //特性 - 无
                default:
                    if (shape != null) {
                        XPrs3d_Drawer shapeDrawer = shape.Attributes();
                        XPrs3d_ShadingAspect ShadingAspect = shapeDrawer.ShadingAspect();
                        XGraphic3d_AspectFillArea3d AspectFillArea3d = shapeDrawer.BasicFillAreaAspect();
                        AspectFillArea3d.SetDistinguishOff();
                        AspectFillArea3d.SetTextureMapOff();
                    }
                    else if (OCCTView.IsObjectSelected() && OCCTContext.SelectedInteractive() is XAIS_InteractiveObject SelectedShape && SelectedShape != null) {
                        XPrs3d_Drawer shapeDrawer = SelectedShape.Attributes();
                        XPrs3d_ShadingAspect ShadingAspect = shapeDrawer.ShadingAspect();
                        XGraphic3d_AspectFillArea3d AspectFillArea3d = shapeDrawer.BasicFillAreaAspect();
                        AspectFillArea3d.SetDistinguishOff();
                        AspectFillArea3d.SetTextureMapOff();
                    }
                    DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"关闭特性", "MainForm", 0);
                    break;
            }
        }
        #endregion

        #region 打开文件
        /// <summary>
        /// 操作 - 打开文件，选择文件
        /// </summary>
        void OperationOpenFile()
        {
            ExplorerNew explorer = new ExplorerNew();
            explorer.FormBorderStyle = FormBorderStyle.None;
            explorer.Location = new Point(3, 3);
            explorer.Width = this.Width - 3;
            explorer.Height = this.Height - 3;
            if (explorer.ShowDialog() == DialogResult.OK) {
                OCCTView.GetInteractiveContext().RemoveAll(true);
                OCCTView.TriedronDisplay(true, XAspect_TypeOfTriedronPosition.Aspect_TOTP_RIGHT_LOWER, 100, 100);
                string FullName = explorer.FullName;
                CurrentModelFormat theFormat = CurrentModelFormat.STEP;
                switch (Path.GetExtension(FullName)?.ToLower()) {
                    case ".stp":
                    case ".step":
                        theFormat = CurrentModelFormat.STEP;
                        this.accElementTLable.Elements.Clear();
                        TranslateModel(FullName, theFormat);
                        break;
                    case ".brep":
                        theFormat = CurrentModelFormat.BREP;
                        OCCTView.TranslateModel(FullName, (int)theFormat, true);
                        break;
                    case ".iges":
                        theFormat = CurrentModelFormat.IGES;
                        OCCTView.TranslateModel(FullName, (int)theFormat, true);
                        break;
                    case ".vrml":
                        theFormat = CurrentModelFormat.VRML;
                        OCCTView.TranslateModel(FullName, (int)theFormat, true);
                        break;
                    case ".png":
                    case ".pmp":
                        theFormat = CurrentModelFormat.IMAGE;
                        OCCTView.TranslateModel(FullName, (int)theFormat, true);
                        break;
                    case ".stl":
                        theFormat = CurrentModelFormat.STL;
                        OCCTView.TranslateModel(FullName, (int)theFormat, true);
                        break;
                    default:
                        return;
                }
                DisplayMessage(Guid.NewGuid().ToString(), "code 156", $"打开文件{Path.GetFileName(FullName)}完成", "MainForm", 0);
            }
        }
        #endregion

        #region 保存文件
        /// <summary>
        /// 操作 - 保存文件
        /// </summary>
        void OperationSaveFile()
        {
            ExplorerNew explorer = new ExplorerNew();
            explorer.SetOperationSave(true);
            explorer.FormBorderStyle = FormBorderStyle.None;
            explorer.Location = new Point(3, 3);
            explorer.Width = this.Width - 3;
            explorer.Height = this.Height - 3;
            if (explorer.ShowDialog() == DialogResult.OK) {
                string FullName = explorer.FullName;
                CurrentModelFormat theFormat = CurrentModelFormat.STEP;
                switch (Path.GetExtension(FullName)?.ToLower()) {
                    case ".stp":
                    case ".step":
                        theFormat = CurrentModelFormat.STEP;
                        OCCTView.TranslateModel(FullName, (int)theFormat, false);
                        break;
                    case ".brep":
                        theFormat = CurrentModelFormat.BREP;
                        OCCTView.TranslateModel(FullName, (int)theFormat, false);
                        break;
                    case ".iges":
                        theFormat = CurrentModelFormat.IGES;
                        OCCTView.TranslateModel(FullName, (int)theFormat, false);
                        break;
                    case ".vrml":
                        theFormat = CurrentModelFormat.VRML;
                        OCCTView.TranslateModel(FullName, (int)theFormat, false);
                        break;
                    case ".png":
                    case ".pmp":
                        theFormat = CurrentModelFormat.IMAGE;
                        OCCTView.TranslateModel(FullName, (int)theFormat, false);
                        break;
                    case ".stl":
                        theFormat = CurrentModelFormat.STL;
                        OCCTView.TranslateModel(FullName, (int)theFormat, false);
                        break;
                    default:
                        return;
                }
                DisplayMessage(Guid.NewGuid().ToString(), "code 163", $"保存文件{Path.GetFileName(FullName)}完成", "MainForm", 0);
            }
        }
        #endregion
        #endregion

        #region 导入导出
        /// <summary>
        /// 导入STEP
        /// </summary>
        /// <param name="theFileName"></param>
        /// <param name="theFormat"></param>
        /// <returns></returns>
        public bool TranslateModel(string theFileName, CurrentModelFormat theFormat)
        {
            XSTEPCAFControl_Reader aReader = new XSTEPCAFControl_Reader();
            aReader.SetColorMode(true);
            aReader.SetNameMode(true);
            XIFSelect_ReturnStatus aStatus = (XIFSelect_ReturnStatus)aReader.ReadFile(theFileName);
            string astorageformat = $"XSTEPCAF_{Guid.NewGuid().ToString()}_XSTEPCAF";
            XTDocStd_Document aDoc = new XTDocStd_Document(astorageformat);
            XXCAFApp_Application anApp = new XXCAFApp_Application();
            anApp.NewDocument(astorageformat, aDoc);
            if (aStatus != XIFSelect_ReturnStatus.IFSelect_RetDone || !aReader.Transfer(aDoc))
                return false;
            int ElementId = 0;
            bool IsBoundaryDraw = true;
            bool BuildElement = false;
            AccordionControlElement RootNode = this.accElementTLable;
            XTDF_Label aRootLabel = aDoc.Main();
            XXCAFDoc_ShapeTool AssemblyShapeTool = XXCAFDoc_DocumentTool.ShapeTool(aRootLabel);
            XTDF_LabelSequence aRootLabels = new XTDF_LabelSequence();
            AssemblyShapeTool.GetFreeShapes(ref aRootLabels);
            XTDF_XIterator aRootIter = aRootLabels.Iterator();
            for (; aRootIter.More(); aRootIter.Next()) {
                XTDF_Label aTDFLabel = aRootIter.Value();
                AccordionControlElement tempElement = AddAccordionElement(RootNode, aTDFLabel, ref ElementId);
                DisplayLabel(AssemblyShapeTool, tempElement, aTDFLabel, ref ElementId, IsBoundaryDraw, new XTopLoc_Location());
                //TDFChildLabel(AssemblyShapeTool, tempElement, aTDFLabel, ref ElementId, IsBoundaryDraw, new XTopLoc_Location());
                BuildElement = true;
            }
            if (!BuildElement) {
                AccordionControlElement tempElement = AddAccordionElement(RootNode, aRootLabel, ref ElementId);
                DisplayLabel(AssemblyShapeTool, tempElement, aRootLabel, ref ElementId, IsBoundaryDraw, new XTopLoc_Location());
                //TDFChildLabel(AssemblyShapeTool, tempElement, aRootLabel, ref ElementId, IsBoundaryDraw, new XTopLoc_Location());
            }
            OCCTView.SetDisplayMode(1, true);
            OCCTView.RedrawView();
            OCCTView.ZoomAllView();
            this.Refresh();
            return true;
        }

        private void XDisplayLabel(XXCAFDoc_ShapeTool AssemblyShapeTool, AccordionControlElement GroupElement, XTDF_Label theLabel, ref int ElementId, bool IsBoundaryDraw, XTopLoc_Location XLocalLocation)
        {
            XTDF_Label aRefLabel = theLabel;
            XXCAFDoc_ShapeTool.GetReferredShape(theLabel, ref aRefLabel);
            if (XXCAFDoc_ShapeTool.IsAssembly(aRefLabel)) {
                //XTopLoc_Location aLoc = XLocalLocation.Multiplied(XXCAFDoc_ShapeTool.GetLocation(theLabel));
                XTDF_ChildIterator aChildIter = new XTDF_ChildIterator(aRefLabel, false);
                for (; aChildIter.More(); aChildIter.Next()) {
                    XTDF_Label RTDFLabel = aChildIter.Value();
                    XTopoDS_Shape currentShape = XXCAFDoc_ShapeTool.GetShape(RTDFLabel);
                    XTopLoc_Location LocalLocation = currentShape.Location();
                    AccordionControlElement tempElement = AddAccordionElement(GroupElement, RTDFLabel, ref ElementId);
                    XDisplayLabel(AssemblyShapeTool, tempElement, RTDFLabel, ref ElementId, IsBoundaryDraw, LocalLocation);
                }
            }
            else {
                GroupElement.Style = ElementStyle.Item;
                Display(theLabel, IsBoundaryDraw, XLocalLocation);
            }
        }

        /// <summary>
        /// 显示TDF_Label
        /// </summary>
        /// <param name="AssemblyShapeTool"></param>
        /// <param name="GroupElement"></param>
        /// <param name="theLabel"></param>
        /// <param name="ElementId"></param>
        /// <param name="IsBoundaryDraw"></param>
        /// <param name="XLocalLocation"></param>
        private void DisplayLabel(XXCAFDoc_ShapeTool AssemblyShapeTool, AccordionControlElement GroupElement, XTDF_Label theLabel, ref int ElementId, bool IsBoundaryDraw, XTopLoc_Location XLocalLocation)
        {
            XTDF_ChildIDIterator ChildIDIterator = new XTDF_ChildIDIterator(theLabel, XXCAFDoc.ShapeRefGUID(), false);
            if (ChildIDIterator.More()) {
                for (; ChildIDIterator.More(); ChildIDIterator.Next()) {
                    XTDF_Attribute ChildIDAttribute = ChildIDIterator.Value();
                    XTDF_Label RTDFLabel = ChildIDAttribute.Label();
                    //XTopLoc_Location aLocalLocation = XLocalLocation.Multiplied(XXCAFDoc_ShapeTool.GetLocation(RTDFLabel));
                    AccordionControlElement tempElement = AddAccordionElement(GroupElement, RTDFLabel, ref ElementId);
                    DisplayLabel(AssemblyShapeTool, tempElement, RTDFLabel, ref ElementId, IsBoundaryDraw, XLocalLocation);
                }
            }
            else {
                XTopoDS_Shape currentShape = XXCAFDoc_ShapeTool.GetShape(theLabel);
                XTopAbs_ShapeEnum _ShapeEnum = currentShape.ShapeType();
                switch (_ShapeEnum) {
                    case XTopAbs_ShapeEnum.TopAbs_COMPOUND:
                    case XTopAbs_ShapeEnum.TopAbs_COMPSOLID:
                        DisplayChildrenLabel(AssemblyShapeTool, GroupElement, currentShape, ref ElementId, IsBoundaryDraw, XLocalLocation);
                        break;
                    case XTopAbs_ShapeEnum.TopAbs_WIRE:
                        break;
                    case XTopAbs_ShapeEnum.TopAbs_SOLID:
                    case XTopAbs_ShapeEnum.TopAbs_FACE:
                    case XTopAbs_ShapeEnum.TopAbs_EDGE:
                    case XTopAbs_ShapeEnum.TopAbs_SHELL:
                    case XTopAbs_ShapeEnum.TopAbs_SHAPE:
                    case XTopAbs_ShapeEnum.TopAbs_VERTEX:
                    default: {
                            GroupElement.Style = ElementStyle.Item;
                            GroupElement.ImageOptions.Image = global::XCAD.Properties.Resources.Img_5101;
                            Display(theLabel, IsBoundaryDraw, XLocalLocation);
                        }
                        break;
                }
            }
        }
        //创建子树(只构建下面一层子树)
        private void DisplayChildrenLabel(XXCAFDoc_ShapeTool AssemblyShapeTool, AccordionControlElement GroupElement, XTopoDS_Shape parentShape, ref int ElementId, bool IsBoundaryDraw, XTopLoc_Location XLocalLocation)
        {
            XAIS_InteractiveContext context = OCCTView.GetInteractiveContext();
            XTopoDS_Iterator iter = new XTopoDS_Iterator(parentShape, true, true);
            for (; iter.More(); iter.Next()) {
                XTopoDS_Shape currentShape = iter.Value();
                XTopLoc_Location LocalLocation = currentShape.Location();
                if (!LocalLocation.IsIdentity())
                    XLocalLocation = LocalLocation;
                else
                    XLocalLocation = new XTopLoc_Location();
                XTopAbs_ShapeEnum _ShapeEnum = currentShape.ShapeType();
                switch (_ShapeEnum) {
                    case XTopAbs_ShapeEnum.TopAbs_COMPOUND:
                    case XTopAbs_ShapeEnum.TopAbs_COMPSOLID: {
                            //DisplayChildrenLabel(AssemblyShapeTool, GroupElement, currentShape, ref ElementId, IsBoundaryDraw, XLocalLocation);
                            XTDF_Label aTDFLabel = new XTDF_Label();
                            AccordionControlElement tempElement = new AccordionControlElement();
                            if (AssemblyShapeTool.FindShape(currentShape, ref aTDFLabel, false)) {
                                tempElement = AddAccordionElement(GroupElement, aTDFLabel, ref ElementId);
                            }
                            else {
                                AccordionControlElement GroupNode = GroupElement;
                                GroupNode = new AccordionControlElement();
                                GroupNode.Name = $"{currentShape.ShapeType().ToString()}_{ ElementId }";
                                GroupNode.Text = currentShape.ShapeType().ToString();
                                GroupNode.Tag = ElementId++;
                                GroupElement.Elements.Add(GroupNode);
                                tempElement = GroupNode;
                            }
                            DisplayChildrenLabel(AssemblyShapeTool, tempElement, currentShape, ref ElementId, IsBoundaryDraw, XLocalLocation);
                        }
                        break;
                    case XTopAbs_ShapeEnum.TopAbs_WIRE:
                        break;
                    case XTopAbs_ShapeEnum.TopAbs_SOLID:
                    case XTopAbs_ShapeEnum.TopAbs_FACE:
                    case XTopAbs_ShapeEnum.TopAbs_EDGE:
                    case XTopAbs_ShapeEnum.TopAbs_SHELL:
                    case XTopAbs_ShapeEnum.TopAbs_SHAPE:
                    case XTopAbs_ShapeEnum.TopAbs_VERTEX:
                    default: {
                            XTDF_Label aTDFLabel = new XTDF_Label();
                            if (AssemblyShapeTool.Search(currentShape, ref aTDFLabel, true, true, true)) {
                                AccordionControlElement tempElement = AddAccordionElement(GroupElement, aTDFLabel, ref ElementId);
                                DisplayLabel(AssemblyShapeTool, tempElement, aTDFLabel, ref ElementId, IsBoundaryDraw, XLocalLocation);
                            }
                            else {
                                GroupElement.Style = ElementStyle.Item;
                                GroupElement.ImageOptions.Image = global::XCAD.Properties.Resources.Img_5101;
                                XAIS_Shape shape = new XAIS_Shape(currentShape);
                                context.Display(shape, true);
                                OCCTView.SetFaceBoundaryDraw(shape, IsBoundaryDraw);
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 子结构查询
        /// </summary>
        /// <param name="AssemblyShapeTool"></param>
        /// <param name="GroupElement"></param>
        /// <param name="theLabel"></param>
        /// <param name="ElementId"></param>
        /// <param name="IsBoundaryDraw"></param>
        private void TDFChildLabel(XXCAFDoc_ShapeTool AssemblyShapeTool, AccordionControlElement GroupElement, XTDF_Label theLabel, ref int ElementId, bool IsBoundaryDraw, XTopLoc_Location XLocalLocation = null)
        {
            XTDF_ChildIDIterator ChildIDIterator = new XTDF_ChildIDIterator(theLabel, XXCAFDoc.ShapeRefGUID(), false);
            if (ChildIDIterator.More()) {
                for (; ChildIDIterator.More(); ChildIDIterator.Next()) {
                    XTDF_Attribute ChildIDAttribute = ChildIDIterator.Value();
                    XTDF_Label RTDFLabel = ChildIDAttribute.Label();
                    AccordionControlElement tempElement = AddAccordionElement(GroupElement, RTDFLabel, ref ElementId);
                    TDFChildLabel(AssemblyShapeTool, tempElement, RTDFLabel, ref ElementId, IsBoundaryDraw, XLocalLocation);
                }
            }
            else {
                XTopoDS_Shape currentShape = XXCAFDoc_ShapeTool.GetShape(theLabel);
                XTopAbs_ShapeEnum _ShapeEnum = currentShape.ShapeType();
                switch (_ShapeEnum) {
                    case XTopAbs_ShapeEnum.TopAbs_COMPOUND:
                        XTopoDS_Iterator iter = new XTopoDS_Iterator(currentShape, true, true);
                        for (; iter.More(); iter.Next()) {
                            XTopoDS_Shape SubShape = iter.Value();
                            XTopLoc_Location LocalLocation = SubShape.Location();
                            XTDF_Label aTDFLabel = new XTDF_Label();
                            if (AssemblyShapeTool.Search(SubShape, ref aTDFLabel, true, true, true)) {
                                AccordionControlElement tempElement = AddAccordionElement(GroupElement, aTDFLabel, ref ElementId);
                                TDFChildLabel(AssemblyShapeTool, tempElement, aTDFLabel, ref ElementId, IsBoundaryDraw, LocalLocation);
                            }
                            else if (AssemblyShapeTool.FindShape(SubShape, ref aTDFLabel, true)) {
                                AccordionControlElement tempElement = AddAccordionElement(GroupElement, aTDFLabel, ref ElementId);
                                TDFChildLabel(AssemblyShapeTool, tempElement, aTDFLabel, ref ElementId, IsBoundaryDraw, LocalLocation);
                            }
                            else if (AssemblyShapeTool.FindShape(SubShape, ref aTDFLabel, false)) {
                                AccordionControlElement tempElement = AddAccordionElement(GroupElement, aTDFLabel, ref ElementId);
                                TDFChildLabel(AssemblyShapeTool, tempElement, aTDFLabel, ref ElementId, IsBoundaryDraw, LocalLocation);
                            }
                            else if (AssemblyShapeTool.SearchUsingMap(SubShape, ref aTDFLabel, true, true)) {
                                AccordionControlElement tempElement = AddAccordionElement(GroupElement, aTDFLabel, ref ElementId);
                                TDFChildLabel(AssemblyShapeTool, tempElement, aTDFLabel, ref ElementId, IsBoundaryDraw, LocalLocation);
                            }
                            else {
                                GroupElement.Style = ElementStyle.Item;
                                Display(theLabel, IsBoundaryDraw, LocalLocation);
                            }
                        }
                        break;
                    case XTopAbs_ShapeEnum.TopAbs_COMPSOLID:
                        break;
                    case XTopAbs_ShapeEnum.TopAbs_SOLID:
                    case XTopAbs_ShapeEnum.TopAbs_FACE:
                    case XTopAbs_ShapeEnum.TopAbs_EDGE:
                    case XTopAbs_ShapeEnum.TopAbs_SHELL: {
                            GroupElement.Style = ElementStyle.Item;
                            Display(theLabel, IsBoundaryDraw, XLocalLocation);
                        }
                        break;
                    case XTopAbs_ShapeEnum.TopAbs_SHAPE:
                        break;
                    case XTopAbs_ShapeEnum.TopAbs_WIRE:
                        break;
                    case XTopAbs_ShapeEnum.TopAbs_VERTEX:
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 构建对象结构
        /// </summary>
        private AccordionControlElement AddAccordionElement(AccordionControlElement GroupElement, XTDF_Label theLabel, ref int ElementId)
        {
            XTCollection_AsciiString aName = new XTCollection_AsciiString();
            XTDF_Attribute aNodeName = new XTDataStd_Name();
            if (theLabel.FindAttribute(XTDataStd_Name.GetID(), ref aNodeName)) {
                XTDataStd_Name XNodeName = aNodeName as XTDataStd_Name;
                aName = new XTCollection_AsciiString(XNodeName.Get());
            }
            if (aName.IsEmpty()) {
                XTDF_Label aRefLabel = new XTDF_Label();
                if (XXCAFDoc_ShapeTool.GetReferredShape(theLabel, ref aRefLabel) && aRefLabel.FindAttribute(XTDataStd_Name.GetID(), ref aNodeName)) {
                    XTDataStd_Name XNodeName = aNodeName as XTDataStd_Name;
                    aName = new XTCollection_AsciiString(XNodeName.Get());
                }
            }
            if (aName.IsEmpty()) {
                XTDF_Tool.Entry(theLabel, ref aName);
            }
            string text = aName.ValueToCString();
            if (string.IsNullOrEmpty(text)) {
                text = theLabel.Tag().ToString();
            }
            XTDataStd_Integer XInteger = new XTDataStd_Integer();
            XTDF_Attribute aInteger = new XTDataStd_Integer();
            if (!theLabel.FindAttribute(XTDataStd_Integer.GetID(), ref aInteger)) {
                XInteger = XTDataStd_Integer.Set(theLabel, ElementId++);
            }
            else {
                XInteger = aInteger as XTDataStd_Integer;
            }
            AccordionControlElement GroupNode = GroupElement;
            GroupNode = new AccordionControlElement();
            GroupNode.Name = $"{text}_{XInteger.Get()}";
            GroupNode.Text = $"{text}";
            GroupNode.Tag = XInteger.Get();
            GroupElement.Elements.Add(GroupNode);
            return GroupNode;
        }

        /// <summary>
        /// 显示图形
        /// </summary>
        /// <param name="theLabel"></param>
        /// <param name="IsBoundaryDraw"></param>
        void Display(XTDF_Label theLabel, bool IsBoundaryDraw, XTopLoc_Location XLocalLocation = null)
        {
            XAIS_InteractiveContext context = OCCTView.GetInteractiveContext();
            XXCAFPrs_AISObject aPrsObject = new XXCAFPrs_AISObject(theLabel);
            if (XLocalLocation != null && !XLocalLocation.IsIdentity())
                aPrsObject.SetLocalTransformation(XLocalLocation);
            XPrs3d_Drawer aDrawer = aPrsObject.Attributes();
            aDrawer.SetFaceBoundaryDraw(IsBoundaryDraw);
            //aPrsObject.SetAttributes(aDrawer);
            //aPrsObject.SetCurrentFacingModel(XAspect_TypeOfFacingModel.Aspect_TOFM_BOTH_SIDE);
            //aPrsObject.SetHilightMode(1);
            context.Display(aPrsObject, true);
            if (XLocalLocation != null && !XLocalLocation.IsIdentity())
                OCCTView.SetLocation1(aPrsObject, XLocalLocation);
            OCCTView.SetFaceBoundaryDraw(aPrsObject, IsBoundaryDraw);
        }
        #endregion

        #region 动画演示
        /// <summary>
        /// 动画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StartAnimation()
        {
            XBRepPrimAPI_MakeBox obj1 = new XBRepPrimAPI_MakeBox(100, 100, 20);
            XAIS_Shape ais_obj1 = new XAIS_Shape(obj1.Shape());
            SetFaceBoundaryAspect(ais_obj1, true);
            AddShape(ais_obj1, true);

            xgp_Trsf end_pnt0 = new xgp_Trsf();
            xgp_Trsf end_pnt1 = new xgp_Trsf();
            end_pnt0.SetTranslation(new xgp_Vec(0.0, 0.0, 0.0));
            end_pnt1.SetTranslation(new xgp_Vec(100.0, 100.0, 0.0));
            XAIS_AnimationObject ais_animation = new XAIS_AnimationObject(new XTCollection_AsciiString($"F1{Guid.NewGuid().ToString()}"), GetInteractiveContext(), ais_obj1, end_pnt0, end_pnt1);
            ais_animation.SetOwnDuration(30.0);
            ais_animation.SetStartPts(0);
            ais_animation.StartTimer(0.0, 1.0, true, false);
            ais_animation.Start(true);
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tag = ais_animation;
            timer.Tick += Timer_Tick;
            timer.Start();
            //var task = new Task(() => {
            //    while (!ais_animation.IsStopped()) {
            //        lock (lockObject)
            //        {
            //            ais_animation.UpdateTimer();
            //            render.UpdateCurrentViewer();
            //        }
            //    }; ais_animation.Stop(); });
            //task.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer timer = sender as Timer;
            XAIS_AnimationObject ais_animation = timer.Tag as XAIS_AnimationObject;
            if (ais_animation != null && !ais_animation.IsStopped()) {
                lock (lockObject) {
                    ais_animation.UpdateTimer();
                    UpdateCurrentViewer();
                }
            };
        }
        #endregion

        #region 几何图形
        /// <summary>
        /// 绘制壳图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MakePrism_Shell(object sender, EventArgs e)
        {
            xgp_Circ CR = new xgp_Circ(new xgp_Ax2(new xgp_Pnt(200.0, 200.0, 0.0), new xgp_Dir(0.0, 0.0, 1.0)), 80.0);
            XTopoDS_Edge REc = new XBRepBuilderAPI_MakeEdge(CR).Edge();
            XTopoDS_Wire RWc = new XBRepBuilderAPI_MakeWire(REc).Wire();
            XBRepBuilderAPI_MakeFace aRMakeFace = new XBRepBuilderAPI_MakeFace(RWc, false);
            //XBRepBuilderAPI_MakeShell tempA = new XBRepBuilderAPI_MakeShell();
            XAIS_Shape WAIS_ECD = new XAIS_Shape(aRMakeFace.Shape());
            SetFaceBoundaryAspect(WAIS_ECD, true);

            AddShape(WAIS_ECD, true);
        }


        /// <summary>
        /// 绘制体图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MakePrism_Solid(object sender, EventArgs e)
        {
            xgp_Pnt P = new xgp_Pnt(0.0, 0.0, 0.0);
            XBRepBuilderAPI_MakeVertex MV1 = new XBRepBuilderAPI_MakeVertex(P);
            XBRepPrimAPI_MakePrism S1 = new XBRepPrimAPI_MakePrism(MV1.Shape(), new xgp_Vec(100.0, 0.0, 0.0), false, true);
            XBRepPrimAPI_MakePrism S2 = new XBRepPrimAPI_MakePrism(S1.Shape(), new xgp_Vec(0.0, 100.0, 0.0), false, true);
            XBRepPrimAPI_MakePrism S3 = new XBRepPrimAPI_MakePrism(S2.Shape(), new xgp_Vec(0.0, 0.0, 100.0), false, true);
            //倒圆
            XBRepFilletAPI_MakeFillet MFBox = new XBRepFilletAPI_MakeFillet(S3.Shape(), XChFi3d_FilletShape.ChFi3d_Rational);
            XTopExp_Explorer exp = new XTopExp_Explorer(S3.Shape(), XTopAbs_ShapeEnum.TopAbs_EDGE, XTopAbs_ShapeEnum.TopAbs_SHAPE);
            while (exp.More()) {
                MFBox.Add(5, XTopoDS.Edge(exp.Current()));
                exp.Next();
            }
            XAIS_Shape WAIS_EC = new XAIS_Shape(MFBox.Shape());
            SetFaceBoundaryAspect(WAIS_EC, true);
            AddShape(WAIS_EC, true);

            //XBRepPrimAPI_MakeBox tempA = new XBRepPrimAPI_MakeBox(200.0, 150.0, 100.0);
            //XBRepPrimAPI_MakeBox tempB = new XBRepPrimAPI_MakeBox(new xgp_Pnt(60, 60, 0), 200.0, 150.0, 100.0);
            //XBRepPrimAPI_MakeBox tempC = new XBRepPrimAPI_MakeBox(new xgp_Pnt(60, 60, 0), new xgp_Pnt(200.0, 150.0, 100.0));
            //XBRepPrimAPI_MakeBox tempD = new XBRepPrimAPI_MakeBox(new xgp_Ax2(new xgp_Pnt(100.0, 80.0, 70.0), new xgp_Dir(1.0, 2.0, 1.0)), 80.0, 90.0, 120.0);

            //XAIS_Shape WAIS_ECA = new XAIS_Shape(tempA.Shape());
            //SetFaceBoundaryAspect(WAIS_ECA, true);

            //XAIS_Shape WAIS_ECB = new XAIS_Shape(tempB.Shape());
            //SetFaceBoundaryAspect(WAIS_ECB, true);

            //XAIS_Shape WAIS_ECC = new XAIS_Shape(tempC.Shape());
            //SetFaceBoundaryAspect(WAIS_ECC, true);

            //XAIS_Shape WAIS_ECD = new XAIS_Shape(tempD.Shape());
            //SetFaceBoundaryAspect(WAIS_ECD, true);

            //render.AddShape(WAIS_ECA, true);
            //render.AddShape(WAIS_ECB, true);
            //render.AddShape(WAIS_ECC, true);
            //render.AddShape(WAIS_ECD, true);
        }

        /// <summary>
        /// 绘制面图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MakePrism_Face(object sender, EventArgs e)
        {
            xgp_Pnt P = new xgp_Pnt(0.0, 0.0, 0.0);
            XBRepBuilderAPI_MakeVertex MV1 = new XBRepBuilderAPI_MakeVertex(P);
            XBRepPrimAPI_MakePrism S1 = new XBRepPrimAPI_MakePrism(MV1.Shape(), new xgp_Vec(100.0, 0.0, 0.0), false, true);
            XBRepPrimAPI_MakePrism S2 = new XBRepPrimAPI_MakePrism(S1.Shape(), new xgp_Vec(0.0, 100.0, 0.0), false, true);
            XAIS_Shape WAIS_EC = new XAIS_Shape(S2.Shape());
            SetFaceBoundaryAspect(WAIS_EC, true);
            AddShape(WAIS_EC, true);

            //xgp_Circ CR = new xgp_Circ(new xgp_Ax2(new xgp_Pnt(200.0, 200.0, 0.0), new xgp_Dir(0.0, 0.0, 1.0)), 80.0);
            //XTopoDS_Edge REc = new XBRepBuilderAPI_MakeEdge(CR).Edge();
            //XTopoDS_Wire RWc = new XBRepBuilderAPI_MakeWire(REc).Wire();
            //XBRepBuilderAPI_MakeFace aRMakeFace = new XBRepBuilderAPI_MakeFace(RWc, false);

            //xgp_Circ Cr = new xgp_Circ(new xgp_Ax2(new xgp_Pnt(200.0, 200.0, 0.0), new xgp_Dir(0.0, 0.0, 1.0)), 60.0);
            //XTopoDS_Edge rEc = new XBRepBuilderAPI_MakeEdge(Cr).Edge();
            //XTopoDS_Wire rWc = new XBRepBuilderAPI_MakeWire(rEc).Wire();
            //XBRepBuilderAPI_MakeFace arMakeFace = new XBRepBuilderAPI_MakeFace(rWc, false);

            //XBRepAlgoAPI_Cut PipeProfile = new XBRepAlgoAPI_Cut(aRMakeFace.Shape(), arMakeFace.Shape());

            //xgp_Vec sVec = new xgp_Vec(0, 0, 1 * 200);
            //XBRepPrimAPI_MakePrism BRPA_MP = new XBRepPrimAPI_MakePrism(PipeProfile.Shape(), sVec, false, false);
            //XAIS_Shape WAIS_EC = new XAIS_Shape(BRPA_MP.Shape());
            //SetFaceBoundaryAspect(WAIS_EC, true);
            //render.AddShape(WAIS_EC, true);
        }



        /// <summary>
        /// 绘制线视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MakePrism_Line(object sender, EventArgs e)
        {
            xgp_Pnt P = new xgp_Pnt(0.0, 0.0, 0.0);
            XBRepBuilderAPI_MakeVertex MV1 = new XBRepBuilderAPI_MakeVertex(P);
            XBRepPrimAPI_MakePrism S1 = new XBRepPrimAPI_MakePrism(MV1.Shape(), new xgp_Vec(100.0, 0.0, 0.0), false, true);
            AddShape(S1.Shape(), true);
        }

        /// <summary>
        /// 绘制点图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MakeVertex(object sender, EventArgs e)
        {
            xgp_Pnt P = new xgp_Pnt(0.0, 0.0, 0.0);
            XBRepBuilderAPI_MakeVertex MV1 = new XBRepBuilderAPI_MakeVertex(P);
            XTopoDS_Vertex V1 = MV1.Vertex();
            AddShape(V1, true);
        }
        #endregion

        #region 演示图形
        /// <summary>
        /// MakeBox
        /// </summary>
        internal void MakeBox()
        {
            xgp_Pnt P = new xgp_Pnt(0, 0, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            XBRepPrimAPI_MakeBox tempMake = new XBRepPrimAPI_MakeBox(Axes, 80, 80, 80);
            AddShape(tempMake.Shape(), true, true);
        }
        /// <summary>
        /// MakeCone
        /// </summary>
        internal void PMakeCone()
        {
            xgp_Pnt P = new xgp_Pnt(200, 0, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            XBRepPrimAPI_MakeCone tempMake = new XBRepPrimAPI_MakeCone(Axes, 80, 40, 100);
            AddShape(tempMake.Shape(), true, true);
        }
        /// <summary>
        /// MakeCylinder
        /// </summary>
        internal void PMakeCylinder()
        {
            xgp_Pnt P = new xgp_Pnt(300, 0, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            XBRepPrimAPI_MakeCylinder tempMake = new XBRepPrimAPI_MakeCylinder(Axes, 80, 100, Math.PI);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeHalfSpace
        /// </summary>
        internal void PMakeHalfSpace()
        {
            xgp_Circ CR = new xgp_Circ(new xgp_Ax2(new xgp_Pnt(400.0, 400.0, 0.0), new xgp_Dir(0.0, 0.0, 1.0)), 80.0);
            XTopoDS_Edge REc = new XBRepBuilderAPI_MakeEdge(CR).Edge();
            XTopoDS_Wire RWc = new XBRepBuilderAPI_MakeWire(REc).Wire();
            XBRepBuilderAPI_MakeFace aRMakeFace = new XBRepBuilderAPI_MakeFace(RWc, false);
            xgp_Pnt P = new xgp_Pnt(400, 400, 400);
            XBRepPrimAPI_MakeHalfSpace tempMake = new XBRepPrimAPI_MakeHalfSpace(aRMakeFace.Face(), P);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakePrism
        /// </summary>
        internal void PMakePrism()
        {
            xgp_Pnt P = new xgp_Pnt(500.0, 0.0, 0.0);
            XBRepBuilderAPI_MakeVertex MV1 = new XBRepBuilderAPI_MakeVertex(P);
            XBRepPrimAPI_MakePrism S1 = new XBRepPrimAPI_MakePrism(MV1.Shape(), new xgp_Vec(100.0, 0.0, 0.0), false, true);
            XBRepPrimAPI_MakePrism S2 = new XBRepPrimAPI_MakePrism(S1.Shape(), new xgp_Vec(0.0, 100.0, 0.0), false, true);
            XBRepPrimAPI_MakePrism tempMake = new XBRepPrimAPI_MakePrism(S2.Shape(), new xgp_Vec(0.0, 0.0, 100.0), false, true);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeRevol
        /// </summary>
        internal void PMakeRevol()
        {
            xgp_Pnt P = new xgp_Pnt(600.0, 0.0, 0.0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            XBRepBuilderAPI_MakeVertex MV1 = new XBRepBuilderAPI_MakeVertex(P);
            XBRepPrimAPI_MakePrism S1 = new XBRepPrimAPI_MakePrism(MV1.Shape(), new xgp_Vec(100.0, 0.0, 0.0), false, true);
            XBRepPrimAPI_MakePrism S2 = new XBRepPrimAPI_MakePrism(S1.Shape(), new xgp_Vec(0.0, 100.0, 0.0), false, true);
            XBRepPrimAPI_MakeRevol tempMake = new XBRepPrimAPI_MakeRevol(S2.Shape(), new xgp_Ax1(P, V), false);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeRevolution
        /// </summary>
        internal void PMakeRevolution()
        {
            xgp_Pnt P = new xgp_Pnt(0, 200.0, 0.0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            XGeom_Line line = new XGeom_Line(P, V);
            XBRepPrimAPI_MakeRevolution tempMake = new XBRepPrimAPI_MakeRevolution(line, 270);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeSphere
        /// </summary>
        internal void PMakeSphere()
        {
            xgp_Pnt P = new xgp_Pnt(100, 200, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            XBRepPrimAPI_MakeSphere tempMake = new XBRepPrimAPI_MakeSphere(Axes, 80);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeTorus
        /// </summary>
        internal void PMakeTorus()
        {
            xgp_Pnt P = new xgp_Pnt(200, 200, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            XBRepPrimAPI_MakeTorus tempMake = new XBRepPrimAPI_MakeTorus(Axes, 80, 40);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeWedge
        /// </summary>
        internal void PMakeWedge()
        {
            xgp_Pnt P = new xgp_Pnt(300, 200, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            XBRepPrimAPI_MakeWedge tempMake = new XBRepPrimAPI_MakeWedge(Axes, 40, 40, 40, 10);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeEdge
        /// </summary>
        internal void BMakeEdge()
        {
            xgp_Pnt P = new xgp_Pnt(400, 200, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            xgp_Circ circ = new xgp_Circ(Axes, 80);
            XBRepBuilderAPI_MakeEdge tempMake = new XBRepBuilderAPI_MakeEdge(circ);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeFace
        /// </summary>
        internal void BMakeFace()
        {
            xgp_Pnt P = new xgp_Pnt(500, 200, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax3 Axes = new xgp_Ax3(P, V);
            xgp_Cylinder C = new xgp_Cylinder(Axes, 80);
            XBRepBuilderAPI_MakeFace tempMake = new XBRepBuilderAPI_MakeFace(C);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeShell
        /// </summary>
        internal void BMakeShell()
        {
            //xgp_Pnt P = new xgp_Pnt(600, 200, 0);
            //xgp_Dir V = new xgp_Dir(0, 0, 1);
            //xgp_Ax3 Axes = new xgp_Ax3(P, V);
            //xgp_Cylinder cylinder = new xgp_Cylinder(Axes, 80);
            //XGeom_CylindricalSurface cylindricalSurface = new XGeom_CylindricalSurface(cylinder);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(cylindricalSurface, true);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeSolid
        /// </summary>
        internal void BMakeSolid()
        {
            //xgp_Pnt P = new xgp_Pnt(600, 400, 0);
            //xgp_Dir V = new xgp_Dir(0, 0, 1);
            //xgp_Ax3 Axes = new xgp_Ax3(P, V);
            //XGeom_CylindricalSurface cylindricalSurface = new XGeom_CylindricalSurface(Axes, 80);
            //XBRepBuilderAPI_MakeShell tempMakeShell = new XBRepBuilderAPI_MakeShell(cylindricalSurface, true);
            //XBRepBuilderAPI_MakeSolid tempMake = new XBRepBuilderAPI_MakeSolid(tempMakeShell.Shell());
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeVertex
        /// </summary>
        internal void BMakeVertex()
        {
            xgp_Pnt P = new xgp_Pnt(0, 300, 0);
            XBRepBuilderAPI_MakeVertex tempMake = new XBRepBuilderAPI_MakeVertex(P);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeWire
        /// </summary>
        internal void BMakeWire()
        {
            xgp_Pnt P = new xgp_Pnt(100, 300, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            xgp_Circ circ = new xgp_Circ(Axes, 80);
            XBRepBuilderAPI_MakeEdge tempEdge = new XBRepBuilderAPI_MakeEdge(circ);
            XBRepBuilderAPI_MakeWire tempMake = new XBRepBuilderAPI_MakeWire(tempEdge.Edge());
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeArcOfCircle
        /// </summary>
        internal void MakeArcOfCircle()
        {
            xgp_Pnt P = new xgp_Pnt(200, 300, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            xgp_Circ circ = new xgp_Circ(Axes, 80);
            XGC_MakeArcOfCircle tempMake = new XGC_MakeArcOfCircle(circ, 90, 270, false);
            XBRepBuilderAPI_MakeEdge tempEdge = new XBRepBuilderAPI_MakeEdge(tempMake.Value());
            AddShape(tempEdge.Edge(), true, true);
        }

        /// <summary>
        /// MakeArcOfEllipse
        /// </summary>
        internal void MakeArcOfEllipse()
        {
            xgp_Pnt P = new xgp_Pnt(400, 300, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            xgp_Elips elips = new xgp_Elips(Axes, 80, 40);
            XGC_MakeArcOfEllipse tempMake = new XGC_MakeArcOfEllipse(elips, 90, 270, false);
            XBRepBuilderAPI_MakeEdge tempEdge = new XBRepBuilderAPI_MakeEdge(tempMake.Value());
            AddShape(tempEdge.Edge(), true, true);
        }

        /// <summary>
        /// MakeArcOfHyperbola
        /// </summary>
        internal void MakeArcOfHyperbola()
        {
            xgp_Pnt P = new xgp_Pnt(600, 300, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            xgp_Hypr hypr = new xgp_Hypr(Axes, 80, 40);
            XGC_MakeArcOfHyperbola tempMake = new XGC_MakeArcOfHyperbola(hypr, 90, 270, false);
            XBRepBuilderAPI_MakeEdge tempEdge = new XBRepBuilderAPI_MakeEdge(tempMake.Value());
            AddShape(tempEdge.Edge(), true, true);
        }

        /// <summary>
        /// MakeArcOfParabola
        /// </summary>
        internal void MakeArcOfParabola()
        {
            xgp_Pnt P = new xgp_Pnt(600, 300, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax1 Axes = new xgp_Ax1(P, V);
            xgp_Pnt F = new xgp_Pnt(650, 300, 300);
            xgp_Parab parab = new xgp_Parab(Axes, F);
            XGC_MakeArcOfParabola tempMake = new XGC_MakeArcOfParabola(parab, 90, 180, false);
            XBRepBuilderAPI_MakeEdge tempEdge = new XBRepBuilderAPI_MakeEdge(tempMake.Value());
            AddShape(tempEdge.Edge(), true, true);
        }

        /// <summary>
        /// MakeCircle
        /// </summary>
        internal void MakeCircle()
        {
            xgp_Pnt P = new xgp_Pnt(600, 300, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax1 Axes = new xgp_Ax1(P, V);
            XGC_MakeCircle tempMake = new XGC_MakeCircle(Axes, 80);
            XBRepBuilderAPI_MakeEdge tempEdge = new XBRepBuilderAPI_MakeEdge(tempMake.Value());
            AddShape(tempEdge.Edge(), true, true);
        }

        /// <summary>
        /// MakeConicalSurface
        /// </summary>
        internal void MakeConicalSurface()
        {
            xgp_Pnt P1 = new xgp_Pnt(700, 300, 0);
            xgp_Pnt P2 = new xgp_Pnt(800, 300, 0);
            XGC_MakeConicalSurface tempSurface = new XGC_MakeConicalSurface(P1, P2, 80, 60);
            XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempSurface.Value(), false);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeCylindricalSurface
        /// </summary>
        internal void MakeCylindricalSurface()
        {
            xgp_Pnt P = new xgp_Pnt(0, 400, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            xgp_Circ circ = new xgp_Circ();
            XGC_MakeCylindricalSurface tempSurface = new XGC_MakeCylindricalSurface(circ);
            XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempSurface.Value(), false);
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeEllipse
        /// </summary>
        internal void MakeEllipse()
        {
            xgp_Pnt P = new xgp_Pnt(0, 400, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            xgp_Elips elips = new xgp_Elips(Axes, 80, 60);
            XGC_MakeEllipse tempSurface = new XGC_MakeEllipse(elips);
            XBRepBuilderAPI_MakeEdge tempMake = new XBRepBuilderAPI_MakeEdge(tempSurface.Value());
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeHyperbola
        /// </summary>
        internal void MakeHyperbola()
        {
            xgp_Pnt P = new xgp_Pnt(0, 400, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax2 Axes = new xgp_Ax2(P, V);
            xgp_Hypr hypr = new xgp_Hypr(Axes, 80, 60);
            XGC_MakeHyperbola tempGC = new XGC_MakeHyperbola(hypr);
            XBRepBuilderAPI_MakeEdge tempMake = new XBRepBuilderAPI_MakeEdge(tempGC.Value());
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeLine
        /// </summary>
        internal void MakeLine()
        {
            xgp_Pnt P = new xgp_Pnt(200, 400, 0);
            xgp_Pnt P1 = new xgp_Pnt(300, 400, 0);
            XGC_MakeLine tempGC = new XGC_MakeLine(P, P1);
            XBRepBuilderAPI_MakeEdge tempMake = new XBRepBuilderAPI_MakeEdge(tempGC.Value());
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakePlane
        /// </summary>
        internal void MakePlane()
        {
            xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax1 Axes = new xgp_Ax1(P, V);
            XGC_MakePlane tempGC = new XGC_MakePlane(Axes);
            XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), 200, 400, 200, 400, false);
            AddShape(tempMake.Shape(), true, true);
        }


        /// <summary>
        /// MakeRotation
        /// </summary>
        internal void MakeRotation()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeScale
        /// </summary>
        internal void MakeScale()
        {
            xgp_Pnt P = new xgp_Pnt(600, 600, 200);
            XGC_MakeScale tempGC = new XGC_MakeScale(P, 1.0);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeSegment
        /// </summary>
        internal void MakeSegment()
        {
            xgp_Pnt P1 = new xgp_Pnt(200, 300, 0);
            xgp_Pnt P2 = new xgp_Pnt(300, 300, 0);
            XGC_MakeSegment tempMake = new XGC_MakeSegment(P1, P2);
            XBRepBuilderAPI_MakeEdge tempEdge = new XBRepBuilderAPI_MakeEdge(tempMake.Value());
            AddShape(tempEdge.Edge(), true, true);
        }

        /// <summary>
        /// Transformation
        /// </summary>
        internal void Transformation()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeTrimmedCone
        /// </summary>
        internal void MakeTrimmedCone()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// MakeTrimmedCylinder
        /// </summary>
        internal void MakeTrimmedCylinder()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeCirc
        /// </summary>
        internal void EMakeCirc()
        {
            xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            xgp_Dir V = new xgp_Dir(1, 0, 0);
            xgp_Ax1 Axes = new xgp_Ax1(P, V);
            xgce_MakeCirc tempGCE = new xgce_MakeCirc(Axes, 270);
            XBRepBuilderAPI_MakeEdge tempMake = new XBRepBuilderAPI_MakeEdge(tempGCE.Value());
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeCirc2d
        /// </summary>
        internal void EMakeCirc2d()
        {
            xgp_Pnt2d P = new xgp_Pnt2d(400, 400);
            xgp_Dir2d V = new xgp_Dir2d(1, 0);
            xgp_Ax2d A = new xgp_Ax2d(P, V);
            xgp_Ax22d Axis = new xgp_Ax22d(A, false);
            xgp_Circ2d Axes = new xgp_Circ2d(Axis, 80);
            xgce_MakeCirc2d tempGCE = new xgce_MakeCirc2d(Axes, 60);
            //XBRepBuilderAPI_MakeEdge tempMake = new XBRepBuilderAPI_MakeEdge(tempGCE.Value());
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeCone
        /// </summary>
        internal void EMakeCone()
        {
            xgp_Pnt P1 = new xgp_Pnt(400, 400, 0);
            xgp_Pnt P2 = new xgp_Pnt(400, 600, 0);
            xgce_MakeCone tempGCE = new xgce_MakeCone(P1, P2, 80, 80);
            XBRepBuilderAPI_MakeFace tempMake = new XBRepBuilderAPI_MakeFace(tempGCE.Value());
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeCylinder
        /// </summary>
        internal void EMakeCylinder()
        {
            xgp_Pnt P1 = new xgp_Pnt(400, 400, 0);
            xgp_Pnt P2 = new xgp_Pnt(400, 600, 0);
            xgp_Cylinder Cyl = new xgp_Cylinder();
            xgce_MakeCylinder tempGCE = new xgce_MakeCylinder(Cyl, 80);
            XBRepBuilderAPI_MakeFace tempMake = new XBRepBuilderAPI_MakeFace(tempGCE.Value());
            AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeDir
        /// </summary>
        internal void EMakeDir()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeDir2d
        /// </summary>
        internal void EMakeDir2d()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeElips
        /// </summary>
        internal void EMakeElips()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeElips2d
        /// </summary>
        internal void EMakeElips2d()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeHypr
        /// </summary>
        internal void EMakeHypr()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeHypr2d
        /// </summary>
        internal void EMakeHypr2d()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeLin
        /// </summary>
        internal void EMakeLin()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeLin2d
        /// </summary>
        internal void EMakeLin2d()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeMirror
        /// </summary>
        internal void EMakeMirror()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeMirror2d
        /// </summary>
        internal void EMakeMirror2d()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeParab
        /// </summary>
        internal void EMakeParab()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeParab2d
        /// </summary>
        internal void EMakeParab2d()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakePln
        /// </summary>
        internal void EMakePln()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeRotation
        /// </summary>
        internal void EMakeRotation()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeRotation2d
        /// </summary>
        internal void EMakeRotation2d()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeScale
        /// </summary>
        internal void EMakeScale()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeScale2d
        /// </summary>
        internal void EMakeScale2d()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeTranslation
        /// </summary>
        internal void EMakeTranslation()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }

        /// <summary>
        /// EMakeTranslation2d
        /// </summary>
        internal void EMakeTranslation2d()
        {
            //xgp_Pnt P = new xgp_Pnt(400, 400, 0);
            //xgp_Dir V = new xgp_Dir(1, 0, 0);
            //xgp_Ax1 Axes = new xgp_Ax1(P, V);
            //XGC_MakeRotation tempGC = new XGC_MakeRotation(Axes, 270);
            //XBRepBuilderAPI_MakeShell tempMake = new XBRepBuilderAPI_MakeShell(tempGC.Value(), false);
            //IRender.AddShape(tempMake.Shape(), true, true);
        }
        #endregion

        #region 设置配置

        /// <summary>
        /// 判断是否选择对象
        /// </summary>
        /// <returns></returns>
        public bool IsObjectSelected {
            get { return OCCTView.IsObjectSelected(); }
        }

        /// <summary>
        /// 立即绘制
        /// </summary>
        public void RedrawView()
        {
            OCCTView.RedrawView();
        }

        /// <summary>
        /// 设置显示模式
        /// Set display mode
        /// </summary>
        /// <param name="aMode">显示模式</param>
        public void SetDisplayMode(int aMode)
        {
            OCCTView.SetDisplayMode(aMode, true);
            OCCTView.RedrawView();
        }

        public int GetDisplayMode()
        {
            return OCCTView.DisplayMode();
        }

        public bool DegenerateMode {
            get { return this.myDegenerateModeIsOn; }
            set { this.myDegenerateModeIsOn = value; }
        }

        public CurrentAction3d Mode {
            get { return this.myCurrentMode; }
            set { this.myCurrentMode = value; }
        }

        public float Zoom {
            set { this.myCurZoom = value; }
        }

        /// <summary>
        /// 修改对象的显示颜色或者背景色
        /// Change the display color or background color of the object
        /// </summary>
        /// <param name="IsObjectColor">是否是对象</param>
        public void ChangeColor(bool IsObjectColor)
        {
            int r, g, b;
            if (IsObjectColor) {
                XQuantity_Color XColor = OCCTView.GetObjectColor();
                r = (int)(XColor.Red() * 255);
                g = (int)(XColor.Green() * 255);
                b = (int)(XColor.Blue() * 255);
            }
            else {
                r = OCCTView.GetBGColR();
                g = OCCTView.GetBGColG();
                b = OCCTView.GetBGColB();
            }
            System.Windows.Forms.ColorDialog ColDlg = new ColorDialog();
            ColDlg.Color = System.Drawing.Color.FromArgb(r, g, b);
            if (ColDlg.ShowDialog() == DialogResult.OK) {
                Color c = ColDlg.Color;
                r = c.R;
                g = c.G;
                b = c.B;
                if (IsObjectColor) {
                    XQuantity_Color XColor = new XQuantity_Color(r, g, b, XQuantity_TypeOfColor.Quantity_TOC_RGB);
                    OCCTView.SetColor(XColor);
                }
                else
                    OCCTView.SetBackgroundColor(r, g, b);
            }
            this.OCCTView.UpdateCurrentViewer();
        }

        /// <summary>
        /// 设置透明度
        /// </summary>
        /// <param name="theTrans"></param>
        public void SetTransparency(int theTrans)
        {
            OCCTView.SetTransparency(theTrans);
            OCCTView.RedrawView();
        }

        /// <summary>
        /// 修改对象的显示颜色或者背景色
        /// Change the display color or background color of the object
        /// </summary>
        /// <param name="IsObjectColor">是否是对象</param>
        /// <param name="color">要修改的颜色</param>
        public void ChangeColor(bool IsObjectColor, Color color)
        {
            int r, g, b;
            if (IsObjectColor) {
                XQuantity_Color XColor = OCCTView.GetObjectColor();
                r = (int)(XColor.Red() * 255);
                g = (int)(XColor.Green() * 255);
                b = (int)(XColor.Blue() * 255);
            }
            else {
                r = OCCTView.GetBGColR();
                g = OCCTView.GetBGColG();
                b = OCCTView.GetBGColB();
            }
            if (color == Color.Empty) {
                System.Windows.Forms.ColorDialog ColDlg = new ColorDialog();
                ColDlg.Color = System.Drawing.Color.FromArgb(r, g, b);
                if (ColDlg.ShowDialog() == DialogResult.OK) {
                    color = ColDlg.Color;
                }
            }
            r = color.R; g = color.G; b = color.B;
            if (IsObjectColor) {
                XQuantity_Color XColor = new XQuantity_Color(r, g, b, XQuantity_TypeOfColor.Quantity_TOC_RGB);
                OCCTView.SetColor(XColor);
            }
            else
                OCCTView.SetBackgroundColor(r, g, b);
            this.OCCTView.UpdateCurrentViewer();

        }

        /// <summary>
        /// 删除对象
        /// delete objects
        /// </summary>
        public void DeleteObjects()
        {
            OCCTView.EraseSelected(true);
            //IE_WinForms.Form1 parent = (IE_WinForms.Form1)this.ParentForm;
            //parent.SelectionChanged();
        }

        /// <summary>
        /// 设置材料
        /// Setting materials
        /// </summary>
        /// <param name="NameOfMaterial"></param>
        public void SetMaterial(XGraphic3d_NameOfMaterial NameOfMaterial)
        {
            OCCTView.SetMaterial((int)NameOfMaterial);
            OCCTView.RedrawView();
        }

        /// <summary>
        /// 删除所有图形
        /// </summary>
        /// <param name="theToUpdateViewer"></param>
        public void RemoveAll(bool theToUpdateViewer)
        {
            XAIS_InteractiveContext context = OCCTView.GetInteractiveContext();
            context.RemoveAll(theToUpdateViewer);
        }


        public void AddShape(XAIS_InteractiveObject theIObj, bool IsFaceBoundaryAspect = true, bool theToUpdateViewer = true)
        {
            XAIS_InteractiveContext context = OCCTView.GetInteractiveContext();
            if (IsFaceBoundaryAspect)
                SetFaceBoundaryAspect(theIObj, IsFaceBoundaryAspect);
            context.Display(theIObj, theToUpdateViewer);
        }

        public XAIS_Shape AddShape(XTopoDS_Shape theTObj, bool IsFaceBoundaryAspect = true, bool theToUpdateViewer = true)
        {
            XAIS_InteractiveContext context = OCCTView.GetInteractiveContext();
            XAIS_Shape theIObj = new XAIS_Shape(theTObj);
            if (IsFaceBoundaryAspect)
                SetFaceBoundaryAspect(theIObj, IsFaceBoundaryAspect);
            context.Display(theIObj, theToUpdateViewer);
            return theIObj;
        }

        public void ClearSelected(bool theToUpdateViewer)
        {
            XAIS_InteractiveContext context = OCCTView.GetInteractiveContext();
            context.ClearSelected(theToUpdateViewer);
        }

        public void UpdateCurrentViewer()
        {
            this.OCCTView.UpdateCurrentViewer();
        }
        public XAIS_InteractiveContext GetInteractiveContext()
        {
            return this.OCCTView.GetInteractiveContext();
        }

        /// <summary>
        ///Get V3d_View
        /// </summary>
        public XV3d_View GetV3dView()
        {
            return this.OCCTView.GetV3dView();
        }


        /// <summary>
        ///Update view
        /// </summary>
        public void UpdateView()
        {
            this.OCCTView.UpdateView();
        }

        /// <summary>
        ///Set computed mode in false
        /// </summary>
        public void SetDegenerateModeOn()
        {

            this.OCCTView.SetDegenerateModeOn();
        }

        /// <summary>
        ///Set computed mode in true
        /// </summary>
        public void SetDegenerateModeOff()
        {
            this.OCCTView.SetDegenerateModeOff();
        }

        /// <summary>
        ///Fit all
        /// </summary>
        public void WindowFitAll(int theXmin, int theYmin, int theXmax, int theYmax)
        {
            this.OCCTView.WindowFitAll(theXmin, theYmin, theXmax, theYmax);
        }

        /// <summary>
        ///Front side
        /// </summary>
        public void FrontView()
        {
            this.OCCTView.FrontView();
        }

        /// <summary>
        ///Top side
        /// </summary>
        public void TopView()
        {
            this.OCCTView.TopView();
        }

        /// <summary>
        ///Left side
        /// </summary>
        public void LeftView()
        {
            this.OCCTView.LeftView();
        }

        /// <summary>
        ///Back side
        /// </summary>
        public void BackView()
        {
            this.OCCTView.BackView();
        }

        /// <summary>
        ///Right side
        /// </summary>
        public void RightView()
        {
            this.OCCTView.RightView();
        }

        /// <summary>
        ///Bottom side
        /// </summary>
        public void BottomView()
        {
            this.OCCTView.BottomView();
        }

        /// <summary>
        ///Axo side
        /// </summary>
        public void AxoView()
        {
            this.OCCTView.AxoView();
        }

        /// <summary>
        ///Zoom in all view
        /// </summary>
        public void ZoomAllView()
        {
            this.OCCTView.ZoomAllView();
        }

        /// <summary>
        ///Scale
        /// </summary>
        public double Scale()
        {
            return this.OCCTView.Scale();
        }


        public void SetLight(bool OnLight)
        {
            this.OCCTView.SetLight(OnLight);
        }

        public void SetGridActivity(bool GridActivity)
        {
            this.OCCTView.SetLight(GridActivity);
        }


        /// <summary>
        ///Reset view
        /// </summary>
        public void Reset()
        {
            this.OCCTView.Reset();
        }

        /// <summary>
        /// 图形显示边框
        /// </summary>
        /// <param name="anInteractive"></param>
        /// <param name="IsBoundaryDraw"></param>
        public void SetFaceBoundaryAspect(XAIS_InteractiveObject anInteractive, bool IsBoundaryDraw)
        {
            XPrs3d_Drawer aDrawer = anInteractive.Attributes();
            aDrawer.SetFaceBoundaryDraw(IsBoundaryDraw);
            if (IsBoundaryDraw) {
                XPrs3d_LineAspect aBoundaryAspect = aDrawer.FaceBoundaryAspect();
                aBoundaryAspect.SetColor(new XQuantity_Color(0.0, 0.0, 0.0, XQuantity_TypeOfColor.Quantity_TOC_RGB));
                aBoundaryAspect.SetTypeOfLine(XAspect_TypeOfLine.Aspect_TOL_SOLID);
                aBoundaryAspect.SetWidth(1.0);
            }
        }

        #endregion

        #region 消息展示

        /// <summary>
        /// 构建消息界面布局
        /// </summary>
        private void InitLayoutMessage()
        {
            this.flyoutMessagePanel.Controls.Clear();
            this.flyoutMessagePanel.BorderStyle = BorderStyle.None;
            this.flyoutMessagePanel.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.TopLeft;
            this.flyoutMessagePanel.Options.VertIndent = this.RWControl.Height - 30;
            this.flyoutMessagePanel.Options.HorzIndent = (this.RWControl.Width - 350) / 2;
            this.flyoutMessagePanel.Height = 20;
            this.flyoutMessagePanel.Width = 350;
            ListBoxControl listBox = new ListBoxControl();
            listBox.HorizontalScrollbar = false;
            listBox.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            listBox.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //listBox.Items.Add(explain);
            //listBox.Dock = DockStyle.Fill;
            listBox.Location = new Point(0, 0);
            listBox.Width = 375;
            listBox.Height = 20;
            listBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.flyoutMessagePanel.Controls.Add(listBox);
            //listBox.BackColor = Color.Transparent;
        }

        /// <summary>
        /// 显示状态信息
        /// </summary>
        /// <param name="strStatusInfo">信息参数</param>
        public void DisplayStatusInfo(string strStatusInfo = "就绪")
        {
            DisplayMessage(Guid.NewGuid().ToString(), "code 2174", strStatusInfo, "MainForm", 0);
        }
        /// <summary>
        /// 更新各种信息消息
        /// </summary>
        /// <param name="logid">信息GUID编号</param>
        /// <param name="code">信息代码或者标题</param>
        /// <param name="explain">说明</param>
        /// <param name="position">位置</param>
        /// <param name="level">错误级别:未知Unknown0;普通消息Message1;警告消息Warning2;错误消息Error3;操作信息Operate4</param>
        public void DisplayMessage(string logid, string code, string explain, string position, int level) {
            ListBoxControl listBox = this.flyoutMessagePanel.Controls[0] as ListBoxControl;
            //listBox.Items.Clear();
            listBox.Items.Add(explain);
            listBox.TopIndex = listBox.ItemCount - 1;
            listBox.SetSelected(listBox.ItemCount - 1, true);
            this.flyoutMessagePanel.ShowPopup();
            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Timer timer = sender as Timer;
            this.flyoutMessagePanel.HidePopup();
            timer.Stop();
        }
        #endregion

        #region 操作事件
        /// <summary>
        /// 窗体大小变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenderWindow_SizeChanged(object sender, EventArgs e)
        {
            if (InitViewer) {
                OCCTView.RedrawView();
                OCCTView.UpdateView();
                OCCTView.UpdateCurrentViewer();
                this.RWControl.Select();
            }
        }
        /// <summary>
        /// 窗体绘制事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenderWindow_Paint(object sender, PaintEventArgs e)
        {
            if (InitViewer) {
                OCCTView.RedrawView();
                OCCTView.UpdateView();
                OCCTView.UpdateCurrentViewer();
                this.RWControl.Select();
            }
        }
        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenderWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift)
                myCurrentPressedKey = CurrentPressedKey.CurPressedKey_Shift;
            else if (e.Control)
                myCurrentPressedKey = CurrentPressedKey.CurPressedKey_Ctrl;
            else if (e.Alt)
                myCurrentPressedKey = CurrentPressedKey.CurPressedKey_Alt;
        }
        /// <summary>
        /// 键盘弹起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenderWindow_KeyUp(object sender, KeyEventArgs e)
        {
            myCurrentPressedKey = CurrentPressedKey.CurPressedKey_Nothing;
            myCurrentMode = CurrentAction3d.CurAction3d_DynamicRotation;
        }
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenderWindow_MouseDown(object sender, MouseEventArgs e)
        {
            //InputHandler.MouseDown(mCanvas, e, Control.ModifierKeys);
            switch (e.Button) {
                case MouseButtons.Left:
                    #region 鼠标左键按下
                    myXmin = e.X; myYmin = e.Y;
                    myXmax = e.X; myYmax = e.Y;
                    if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl)
                        // start the dinamic zooming....
                        myCurrentMode = CurrentAction3d.CurAction3d_Nothing;
                    else {
                        switch (myCurrentMode) {
                            case CurrentAction3d.CurAction3d_Nothing:
                                if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                                    MultiDragEvent(myXmax, myYmax, -1);
                                else
                                    DragEvent(myXmax, myYmax, -1);
                                break;
                            case CurrentAction3d.CurAction3d_DynamicRotation:
                                if (!myDegenerateModeIsOn)
                                    OCCTView.SetDegenerateModeOn();
                                //start the rotation
                                OCCTView.StartRotation(e.X, e.Y);
                                break;
                            case CurrentAction3d.CurAction3d_WindowZooming:
                                this.Cursor = System.Windows.Forms.Cursors.Hand;
                                break;
                            default:
                                break;
                        }
                    }
                    #endregion
                    break;
                case MouseButtons.Right:
                    #region 鼠标右键按下
                    //MessageBox.Show("right mouse button is down");
                    if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl) {
                        if (!myDegenerateModeIsOn)
                            OCCTView.SetDegenerateModeOn();
                        OCCTView.StartRotation(e.X, e.Y);
                    }
                    //else
                    //    Popup(e.X, e.Y);
                    #endregion
                    break;
                case MouseButtons.Middle:
                    #region 鼠标中键按下
                    {
                        if (!myDegenerateModeIsOn)
                            OCCTView.SetDegenerateModeOn();
                        myCurrentMode = CurrentAction3d.CurAction3d_DynamicPanning;
                    }
                    #endregion
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 鼠标弹起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void RenderWindow_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button) {
                case MouseButtons.Left:
                    #region 鼠标左键弹起
                    if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl) {
                        if (e.X == myXmin && e.Y == myYmin) {
                            myXmax = e.X; myYmax = e.Y;
                            if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                                MultiInputEvent(myXmax, myYmax);
                            else
                                InputEvent(myXmax, myYmax);
                        }
                        else {
                            myXmax = e.X; myYmax = e.Y;
                            DrawRectangle(false);
                            if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                                MultiDragEvent(myXmax, myYmax, 1);
                            else
                                DragEvent(myXmax, myYmax, 1);
                        }
                        myCurrentMode = CurrentAction3d.CurAction3d_DynamicRotation;
                        return;
                    }
                    switch (myCurrentMode) {
                        case CurrentAction3d.CurAction3d_Nothing:
                            if (e.X == myXmin && e.Y == myYmin) {
                                myXmax = e.X; myYmax = e.Y;
                                if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                                    MultiInputEvent(myXmax, myYmax);
                                else
                                    InputEvent(myXmax, myYmax);
                            }
                            else {
                                myXmax = e.X; myYmax = e.Y;
                                DrawRectangle(false);
                                if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                                    MultiDragEvent(myXmax, myYmax, 1);
                                else
                                    DragEvent(myXmax, myYmax, 1);
                            }
                            break;
                        case CurrentAction3d.CurAction3d_DynamicZooming:
                            myCurrentMode = CurrentAction3d.CurAction3d_DynamicZooming;
                            break;
                        case CurrentAction3d.CurAction3d_WindowZooming:
                            myXmax = e.X; myYmax = e.Y;
                            DrawRectangle(false);
                            int ValZWMin = 1;
                            if (Math.Abs(myXmax - myXmin) > ValZWMin && Math.Abs(myXmax - myYmax) > ValZWMin)
                                OCCTView.WindowFitAll(myXmin, myYmin, myXmax, myYmax);
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                            //IE_WinForms.Form1 f = (IE_WinForms.Form1)this.ParentForm;
                            //f.SelectionChanged();
                            myCurrentMode = CurrentAction3d.CurAction3d_WindowZooming;
                            break;
                        case CurrentAction3d.CurAction3d_DynamicPanning:
                            myCurrentMode = CurrentAction3d.CurAction3d_DynamicPanning;
                            break;
                        case CurrentAction3d.CurAction3d_GlobalPanning:
                            OCCTView.Place(e.X, e.Y, myCurZoom);
                            myCurrentMode = CurrentAction3d.CurAction3d_GlobalPanning;
                            break;
                        case CurrentAction3d.CurAction3d_DynamicRotation:
                            myCurrentMode = CurrentAction3d.CurAction3d_DynamicRotation;
                            if (e.X == myXmin && e.Y == myYmin) {
                                myXmax = e.X; myYmax = e.Y;
                                if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                                    MultiInputEvent(myXmax, myYmax);
                                else
                                    InputEvent(myXmax, myYmax);
                            }
                            else if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift) {
                                myXmax = e.X; myYmax = e.Y;
                                MultiDragEvent(myXmax, myYmax, 1);
                            }
                            if (!myDegenerateModeIsOn) {
                                OCCTView.SetDegenerateModeOff();
                                myDegenerateModeIsOn = false;
                            }
                            else {
                                OCCTView.SetDegenerateModeOn();
                                myDegenerateModeIsOn = true;
                            }
                            break;
                        default:
                            break;

                    }
                    #endregion
                    break;
                case MouseButtons.Right:
                    #region 鼠标右键弹起
                    if (!myDegenerateModeIsOn) {
                        OCCTView.SetDegenerateModeOff();
                        myDegenerateModeIsOn = false;
                    }
                    else {
                        OCCTView.SetDegenerateModeOn();
                        myDegenerateModeIsOn = true;
                    }
                    #endregion
                    break;
                case MouseButtons.Middle:
                    #region 鼠标中键弹出
                    if (!myDegenerateModeIsOn) {
                        OCCTView.SetDegenerateModeOff();
                        myDegenerateModeIsOn = false;
                    }
                    else {
                        OCCTView.SetDegenerateModeOn();
                        myDegenerateModeIsOn = true;
                    }
                    myCurrentMode = CurrentAction3d.CurAction3d_DynamicRotation;
                    #endregion
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void RenderWindow_MouseMove(object sender, MouseEventArgs e)
        {
            //InputHandler.MouseMove(mCanvas, e, Control.ModifierKeys);
            if (e.Button == MouseButtons.Left) //left button is pressed
            {
                if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl) {
                    //OCCTView.Zoom(myXmax, myYmax, e.X, e.Y);
                    //myXmax = e.X; myYmax = e.Y;
                    DrawRectangle(false);
                    myXmax = e.X; myYmax = e.Y;
                    DrawRectangle(true);
                }
                else {
                    switch (myCurrentMode) {
                        case CurrentAction3d.CurAction3d_Nothing:
                            DrawRectangle(false);
                            myXmax = e.X; myYmax = e.Y;
                            DrawRectangle(true);
                            break;
                        case CurrentAction3d.CurAction3d_DynamicZooming:
                            OCCTView.Zoom(myXmax, myYmax, e.X, e.Y);
                            myXmax = e.X; myYmax = e.Y;
                            break;
                        case CurrentAction3d.CurAction3d_WindowZooming:
                            DrawRectangle(false);
                            myXmax = e.X; myYmax = e.Y;
                            DrawRectangle(true);//add brush here
                            break;
                        case CurrentAction3d.CurAction3d_DynamicPanning:
                            OCCTView.Pan(e.X - myXmax, myYmax - e.Y);
                            myXmax = e.X; myYmax = e.Y;
                            break;
                        case CurrentAction3d.CurAction3d_GlobalPanning:
                            break;
                        case CurrentAction3d.CurAction3d_DynamicRotation:
                            OCCTView.Rotation(e.X, e.Y);
                            OCCTView.RedrawView();
                            break;
                        default:
                            break;
                    }
                }
            } // e.Button == MouseButtons.Left
            else if (e.Button == MouseButtons.Middle) {
                OCCTView.Pan(e.X - myXmax, myYmax - e.Y);
                myXmax = e.X; myYmax = e.Y;
            }//e.Button=MouseButtons.Middle
            else if (e.Button == MouseButtons.Right) //right button is pressed
            {
                if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl)
                    OCCTView.Rotation(e.X, e.Y);
            }
            else // no buttons are pressed
            {
                myXmax = e.X; myYmax = e.Y;
                if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                    MultiMoveEvent(e.X, e.Y);
                else
                    MoveEvent(e.X, e.Y);
            }
        }
        /// <summary>
        /// 鼠标滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void RenderWindow_MouseWheel(object sender, MouseEventArgs e)
        {
            //InputHandler.MouseWheel(mCanvas, e, Control.ModifierKeys);
            int addsd = 1;
            if (e.Delta > 0) addsd = -1;
            else addsd = 1;
            OCCTView.Zoom(e.X + addsd, e.Y, e.X, e.Y);
            myXmax = e.X; myYmax = e.Y;
        }

        protected void MultiDragEvent(int x, int y, int theState)
        {
            if (theState == -1) {
                theButtonDownX = x;
                theButtonDownY = y;
            }
            else if (theState == 1)
                OCCTView.ShiftSelect(Math.Min(theButtonDownX, x), Math.Min(theButtonDownY, y),
                        Math.Max(theButtonDownX, x), Math.Max(theButtonDownY, y));
        }

        protected void DragEvent(int x, int y, int theState)
        {
            if (theState == -1) //mouse is down
            {
                theButtonDownX = x;
                theButtonDownY = y;
            }
            else if (theState == 1) //mouse is up
            {
                OCCTView.Select(Math.Min(theButtonDownX, x), Math.Min(theButtonDownY, y),
                        Math.Max(theButtonDownX, x), Math.Max(theButtonDownY, y));
            }
        }

        protected void MultiInputEvent(int x, int y)
        {
            OCCTView.ShiftSelect();
        }

        protected void InputEvent(int x, int y)
        {
            OCCTView.Select();
        }

        private void DrawRectangle(bool draw)
        {
            Graphics gr = Graphics.FromHwnd(this.RWControl.Handle);
            System.Drawing.Pen p = null;
            if (this.IsRectVisible || (!draw))//erase the rect
            {
                int r = OCCTView.GetBGColR();
                int g = OCCTView.GetBGColG();
                int b = OCCTView.GetBGColB();
                p = new Pen(System.Drawing.Color.FromArgb(r, g, b));
                this.IsRectVisible = false;
                this.OCCTView.UpdateView();
            }
            else if (draw) {
                p = new Pen(System.Drawing.Color.White);
                this.IsRectVisible = true;
            }
            if (p == null)
                return;
            int x = Math.Min(this.myXmin, this.myXmax);
            int y = Math.Min(this.myYmin, this.myYmax);
            gr.DrawRectangle(p, x, y, Math.Abs(myXmax - myXmin), Math.Abs(myYmax - myYmin));
            this.theRectDownX = Math.Max(this.myXmin, this.myXmax);
            this.theRectDownY = Math.Max(this.myYmin, this.myYmax);
        }

        protected void MultiMoveEvent(int x, int y)
        {
            OCCTView.MoveTo(x, y);
        }

        protected void MoveEvent(int x, int y)
        {
            OCCTView.MoveTo(x, y);
        }
        #endregion

        #region 字段属性
        protected CurrentAction3d myCurrentMode;
        protected CurrentPressedKey myCurrentPressedKey;
        protected float myCurZoom;
        protected bool myDegenerateModeIsOn;
        protected int myXmin;
        protected int myYmin;
        protected int myXmax;
        protected int myYmax;
        protected int theButtonDownX;
        protected int theButtonDownY;
        // for erasing of rectangle
        protected int theRectDownX;
        protected int theRectDownY;
        protected bool IsRectVisible;

        public OCCTProxy OCCTView { get; set; } = null;
        public XAIS_InteractiveContext OCCTContext { get; set; } = null;
        public bool InitViewer { get; set; } = false;

        private static object lockObject = new object();
        #endregion
    }
}
