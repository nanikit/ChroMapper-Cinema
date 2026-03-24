using System.Reflection;
using UnityEngine;

namespace ChroMapper_Cinema;

[Plugin("Cinema")]
public class Plugin {
	public static Cinema controller;

	[Init]
	private void Init() {
		var assembly = typeof(Plugin).Assembly;
		var assemblyName = assembly.GetName();
		var informationalVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "null";
		var moduleVersionId = assembly.ManifestModule.ModuleVersionId;

		Debug.Log(
			$"[Cinema] Plugin assembly loaded: name='{assemblyName.Name}', version='{assemblyName.Version}', " +
			$"informationalVersion='{informationalVersion}', mvid='{moduleVersionId}', location='{assembly.Location}'");
		controller = new Cinema();

		LoadInitialMap.OnLevelLoaded += LevelLoaded;
	}

	private void LevelLoaded() {
		var atsc = Object.FindAnyObjectByType<AudioTimeSyncController>();
		var descriptor = Object.FindAnyObjectByType<EnvironmentDescriptor>();
		controller.Init(atsc, descriptor.gameObject);
	}

	[Exit]
	private void Exit() {

	}
}
