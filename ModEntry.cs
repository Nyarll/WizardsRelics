using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

using Godot;


[ModInitializer(nameof(Initialize))]
public partial class ModEntry : Node
{
	public const string ModId = "WizardsRelics";
	public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

	public static void Initialize()
	{
		Logger.Info("Wizards Relics mod: Initialize called.");
		Harmony harmony = new(ModId);
		harmony.PatchAll();
		Logger.Info("Wizards Relics mod: Initialize end.");
	}
}
