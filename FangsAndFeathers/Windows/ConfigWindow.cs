using System;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Lumina.Excel.Sheets;

namespace FangsAndFeathers.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;

    // We give this window a constant ID using ###
    // This allows for labels being dynamic, like "{FPS Counter}fps###XYZ counter window",
    // and the window ID will always be "###XYZ counter window" for ImGui
    public ConfigWindow(Plugin plugin) : base("A Wonderful Configuration Window###With a constant ID")
    {
        Flags = ImGuiWindowFlags.NoCollapse;

        Size = new Vector2(1000, 1000);
        SizeCondition = ImGuiCond.Always;

        Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void PreDraw()
    {
        // Flags must be added or removed before Draw() is being called, or they won't apply
        if (Configuration.IsConfigWindowMovable)
        {
            Flags &= ~ImGuiWindowFlags.NoMove;
        }
        else
        {
            Flags |= ImGuiWindowFlags.NoMove;
        }
    }

    public override void Draw()
    {
        // // can't ref a property, so use a local copy
        // var configValue = Configuration.SomePropertyToBeSavedAndWithADefault;
        // if (ImGui.Checkbox("Random Config Bool", ref configValue))
        // {
        //     Configuration.SomePropertyToBeSavedAndWithADefault = configValue;
        //     // can save immediately on change, if you don't want to provide a "Save and Close" button
        //     Configuration.Save();
        // }
        //
        // var movable = Configuration.IsConfigWindowMovable;
        // if (ImGui.Checkbox("Movable Config Window", ref movable))
        // {
        //     Configuration.IsConfigWindowMovable = movable;
        //     Configuration.Save();
        // }
        //
        // var teststring = Configuration.Test;
        // if (ImGui.InputText("Test Text", ref teststring, 60))
        // {
        //     Configuration.Test = teststring;
        //     Configuration.Save();
        // }
        //
        // if (ImGui.Button("TTTTTT"))
        // {
        //     //Plugin.CommandManager.ProcessCommand("/tp home");
        //     var a = Plugin.AetheryteList;
        //     //a.Last().AetheryteData.Value.PlaceName.Value
        //     var b = Plugin.DataManager.GetExcelSheet<BeastTribe>();
        //
        //     var c = Plugin.DataManager.GetExcelSheet<Description>();
        //     var txtb = "";
        //     foreach (var x in b)
        //     {
        //        if (x.RowId > 0)
        //        {
        //            txtb += x.Name.ExtractText() + "-" + x.RowId + "\n";
        //            var gerge = x.Expansion.Value.Name.ExtractText();
        //        }
        //        var aasasdasddasd = "asdasd";
        //     }
        //     
        //     var txta = "";
        //     foreach (var x in a)
        //     {
        //         if (x.AetheryteId > 0)
        //         {
        //             txta += x.AetheryteData.Value.PlaceName.Value.Name.ExtractText() + "-" + x.AetheryteId + "\n";
        //             var gerge = x;
        //         }
        //     }
        //     
        //     var aasdasd = "asdasd";
        //
        // }
        
        // Anzahl der Spalten und Flags für die Tabelle
        if (ImGui.BeginTable("ExampleTable", 2, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
        {
            ImGui.TableSetupColumn("Beast Tribe");
            ImGui.TableSetupColumn("Aetheryte");
            ImGui.TableHeadersRow();

            foreach (var keyValuePair in Configuration.AetheryteBeastTribeMap)
            {
                ImGui.TableNextRow();
                
                var beasttribe = Plugin.DataManager.GetExcelSheet<BeastTribe>()
                                       .TryGetRow(keyValuePair.Key, out BeastTribe row);
                if (beasttribe)
                {
                    string bt = row.Name.ExtractText();
                    ImGui.TableNextColumn(); ImGui.Text(char.ToUpper(bt[0]) + bt.Substring(1));
                }
                else
                {
                    ImGui.TableNextColumn(); ImGui.Text("ERROR");
                }

                var aetheryte = Plugin.AetheryteList.ToList().Find(entry => entry.AetheryteId == keyValuePair.Value);
                if (aetheryte != null)
                {
                    string ar = aetheryte.AetheryteData.Value.PlaceName.Value.Name.ExtractText();

                    ImGui.TableNextColumn();                     
                    if (ImGui.Button(char.ToUpper(ar[0]) + ar.Substring(1)))
                    {
                        Plugin.CommandManager.ProcessCommand("/tp " + ar);
                    }
                }
                else
                {
                    ImGui.TableNextColumn(); ImGui.Text("ERROR");
                }
            }
            ImGui.EndTable();
        }
    }
}
