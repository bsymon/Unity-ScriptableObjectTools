using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Game.Tools.Events {

public class EventDebuggerWindow : EditorWindow {
	
	public struct GameEventHistoric {
		public float time;
		public System.DateTime date;
		public GameObject invoker;
	}
	
	// -- //
	
	[MenuItem("Tools/Events Debugger")]
	static void Init() {
		EventDebuggerWindow window = EditorWindow.GetWindow(typeof(EventDebuggerWindow)) as EventDebuggerWindow;
		window.Show();
	}
	
	// -- //
	
	GUISkin skin;
	GUIStyle normalEventStyle = GUIStyle.none;
	GUIStyle pokedEventStyle  = GUIStyle.none;
	
	List<GameEvent> gameEvents = new List<GameEvent>();
	Dictionary<GameEvent, float> pokedEvents                      = new Dictionary<GameEvent, float>();
	Dictionary<GameEvent, List<GameEventHistoric>> eventsHistoric = new Dictionary<GameEvent, List<GameEventHistoric>>();
	
	// -- //
	
	void OnEnable() {
		LoadSkin();
		LoadGameEventAssets();
		RegisterGameEvents();
	}
	
	void OnDisable() {
		UnregisterGameEvents();
		gameEvents.Clear();
		pokedEvents.Clear();
		eventsHistoric.Clear();
	}
	
	void OnGUI() {
		Repaint(); // TODO remove
		
		for(int i = 0; i < gameEvents.Count; ++i) {
			GameEventGUI(gameEvents[i]);
		}
	}
	
	// -- //
	
	void GameEventGUI(GameEvent theEvent) {
		EditorGUILayout.LabelField(theEvent.name, IsPoked(theEvent) ? pokedEventStyle : normalEventStyle);
		
		GameEventHistoricGUI(theEvent);
	}
	
	void GameEventHistoricGUI(GameEvent theEvent) {
		EditorGUI.indentLevel ++;
		
		List<GameEventHistoric> historic;
		
		if(eventsHistoric.TryGetValue(theEvent, out historic)) {
			for(int i = 0; i < historic.Count; ++i) {
				EditorGUILayout.LabelField(historic[i].date.ToString("T"), "" + historic[i].invoker);
			}
		}
		
		EditorGUI.indentLevel --;
	}
	
	// -- //
	
	void LoadSkin() {
		skin = Resources.Load("Events/skin") as GUISkin;
		
		normalEventStyle = skin.FindStyle("NormalEvent");
		pokedEventStyle  = skin.FindStyle("PokedEvent");
	}
	
	void LoadGameEventAssets() {
		string[] assetsGUID = AssetDatabase.FindAssets("t:GameEvent");
		
		for(int i = 0; i < assetsGUID.Length; ++i) {
			gameEvents.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(assetsGUID[i]), typeof(GameEvent)) as GameEvent);
		}
	}
	
	void RegisterGameEvents() {
		for(int i = 0; i < gameEvents.Count; ++i) {
			gameEvents[i].AddListener(OnEventTriggered);
		}
	}
	
	void UnregisterGameEvents() {
		for(int i = 0; i < gameEvents.Count; ++i) {
			gameEvents[i].RemoveListener(OnEventTriggered);
		}
	}
	
	bool IsPoked(GameEvent theEvent) {
		return pokedEvents.ContainsKey(theEvent) && EditorApplication.timeSinceStartup - pokedEvents[theEvent] <= 3f;
	}
	
	// -- EVENTS -- //
	
	void OnEventTriggered(GameEvent theEvent) {
		if(!pokedEvents.ContainsKey(theEvent)) {
			pokedEvents.Add(theEvent, 0f);
		}
		
		pokedEvents[theEvent] = (float) EditorApplication.timeSinceStartup;
		
		// Set historic
		List<GameEventHistoric> historic;
		
		if(!eventsHistoric.TryGetValue(theEvent, out historic)) {
			historic = new List<GameEventHistoric>();
			eventsHistoric.Add(theEvent, historic);
		}
		
		historic.Insert(0, new GameEventHistoric() {
			time    = (float) EditorApplication.timeSinceStartup,
			date    = System.DateTime.Now,
			invoker = theEvent.Invoker
		});
	}
	
}

}