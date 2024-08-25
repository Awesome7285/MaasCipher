using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using Math = ExMath;

public class MaasCipher : MonoBehaviour {

   public KMBombInfo Bomb;
   public KMAudio Audio;

   public TextMesh Display;
   public TextMesh InputDisplay;

   public KMSelectable[] KeyboardButtons;
   public KMSelectable KeyboardBackspace;
   public KMSelectable SubmitButton;

   public AudioClip KeyPressSound;
   public AudioClip SolveSound;

   static int ModuleIdCounter = 1;
   int moduleId;
   private bool ModuleSolved;

   private string theWord = "";
   private string[] _allWords = {
      "ABLE", "ACHE", "ACID", "ACNE", "ACRE", "AGED", "AIDE", "AKIN", "ALAS", "ALLY", "ALSO", "AMEN", "AMID", "APEX", "AQUA", "ARCH", "AREA", "ARID", "ARMY", "ATOM", "AUNT", "AURA", "AWAY", "AXES", "AXIS", "AXLE",
      "BABY", "BACK", "BAIL", "BAIT", "BAKE", "BALD", "BALL", "BAND", "BANG", "BANK", "BARE", "BARK", "BARN", "BASE", "BASS", "BATH", "BATS", "BEAD", "BEAM", "BEAN", "BEAR", "BEAT", "BEEF", "BEEN", "BEER", "BELL", "BELT", "BEND", "BENT", "BEST", "BIAS", "BIKE", "BILE", "BILL", "BIND", "BIRD", "BITE", "BLAH", "BLEW", "BLOW", "BLUE", "BLUR", "BOAR", "BOAT", "BODY", "BOIL", "BOLD", "BOLT", "BOMB", "BOND", "BONE", "BONY", "BOOK", "BOOM", "BOOT", "BORE", "BORN", "BOSS", "BOTH", "BOUT", "BOWL", "BROW", "BULB", "BULK", "BULL", "BUMP", "BURN", "BURY", "BUSH", "BUST", "BUSY", "BUTT", "BUZZ",
      "CAFE", "CAGE", "CAKE", "CALF", "CALL", "CALM", "CAME", "CAMP", "CANE", "CAPE", "CARD", "CARE", "CARP", "CART", "CASE", "CASH", "CAST", "CAVE", "CELL", "CHAP", "CHAT", "CHEF", "CHIN", "CHIP", "CHOP", "CITY", "CLAD", "CLAM", "CLAN", "CLAP", "CLAW", "CLAY", "CLIP", "CLOG", "CLUB", "CLUE", "COAL", "COAT", "COCK", "CODE", "COIL", "COIN", "COLD", "COMB", "COME", "CONE", "COOK", "COOL", "COPE", "COPY", "CORD", "CORE", "CORK", "CORN", "COST", "COSY", "COUP", "COZY", "CRAB", "CREW", "CRIB", "CROP", "CROW", "CUBE", "CULT", "CURB", "CURE", "CURL", "CUTE",
      "DAFT", "DAMP", "DARE", "DARK", "DART", "DASH", "DATA", "DATE", "DAWN", "DAYS", "DEAD", "DEAF", "DEAL", "DEAR", "DEBT", "DECK", "DEED", "DEEP", "DEER", "DENT", "DENY", "DESK", "DIAL", "DICE", "DIET", "DINE", "DIRE", "DIRT", "DISC", "DISH", "DISK", "DIVE", "DOCK", "DOLE", "DOLL", "DOME", "DONE", "DOOR", "DOSE", "DOVE", "DOWN", "DRAG", "DRAW", "DREW", "DRIP", "DROP", "DRUG", "DRUM", "DUAL", "DUCK", "DUEL", "DUET", "DULL", "DULY", "DUMB", "DUMP", "DUSK", "DUST", "DUTY",
      "EACH", "EARN", "EARS", "EASE", "EAST", "EASY", "EATS", "ECHO", "EDGE", "EDIT", "ELSE", "ENVY", "EPIC", "EURO", "EVEN", "EVER", "EVIL", "EXAM", "EXIT", "EYED", "EYES",
      "FACE", "FACT", "FADE", "FAIL", "FAIR", "FAKE", "FALL", "FAME", "FARE", "FARM", "FAST", "FATE", "FEAR", "FEAT", "FEED", "FEEL", "FEET", "FELL", "FELT", "FILE", "FILL", "FILM", "FIND", "FINE", "FIRE", "FIRM", "FISH", "FIST", "FIVE", "FLAG", "FLAP", "FLAT", "FLAW", "FLED", "FLEE", "FLEW", "FLEX", "FLIP", "FLOW", "FLUX", "FOAM", "FOIL", "FOLD", "FOLK", "FOND", "FONT", "FOOD", "FOOL", "FOOT", "FORD", "FORK", "FORM", "FORT", "FOUL", "FOUR", "FREE", "FROG", "FROM", "FUEL", "FULL", "FUND", "FURY", "FUSE", "FUSS",
      "GAIN", "GALA", "GALL", "GAME", "GANG", "GASP", "GATE", "GAVE", "GAZE", "GEAR", "GENE", "GERM", "GIFT", "GILL", "GILT", "GIRL", "GIVE", "GLAD", "GLEE", "GLOW", "GLUE", "GOAL", "GOAT", "GOES", "GOLD", "GOLF", "GONE", "GONG", "GOOD", "GOSH", "GOWN", "GRAB", "GRAM", "GRAY", "GREW", "GREY", "GRID", "GRIM", "GRIN", "GRIP", "GRIT", "GROW", "GUST",
      "HAIL", "HAIR", "HALF", "HALL", "HALT", "HAND", "HANG", "HARD", "HARE", "HARM", "HATE", "HAUL", "HAVE", "HAWK", "HAZE", "HEAD", "HEAL", "HEAP", "HEAR", "HEAT", "HEEL", "HEIR", "HELD", "HELL", "HELP", "HERB", "HERD", "HERE", "HERO", "HERS", "HIDE", "HIGH", "HIKE", "HILL", "HINT", "HIRE", "HOLD", "HOLE", "HOLY", "HOME", "HOOD", "HOOK", "HOPE", "HORN", "HOSE", "HOST", "HOUR", "HOWL", "HUGE", "HULL", "HUNG", "HUNT", "HURT", "HUSH", "HYMN", "HYPE",
      "ICED", "ICON", "IDEA", "IDLE", "IDOL", "INCH", "INFO", "INTO", "IRON", "ITCH", "ITEM",
      "JACK", "JAIL", "JARS", "JAZZ", "JINX", "JOBS", "JOIN", "JOKE", "JUMP", "JUNK", "JURY", "JUST",
      "KEEN", "KEEP", "KELP", "KEPT", "KICK", "KILL", "KIND", "KING", "KISS", "KITE", "KIWI", "KNEE", "KNEW", "KNIT", "KNOB", "KNOT", "KNOW",
      "LACE", "LACK", "LADY", "LAID", "LAIR", "LAKE", "LAMB", "LAMP", "LAND", "LANE", "LAST", "LATE", "LAVA", "LAWN", "LAZY", "LEAD", "LEAF", "LEAK", "LEAN", "LEAP", "LEFT", "LEND", "LENS", "LESS", "LEST", "LEVY", "LIAR", "LIED", "LIFE", "LIFT", "LIKE", "LIMB", "LIME", "LINE", "LINK", "LION", "LIST", "LIVE", "LOAD", "LOAF", "LOAN", "LOCK", "LOFT", "LOGO", "LONE", "LONG", "LOOK", "LOOP", "LORD", "LOSE", "LOSS", "LOST", "LOTS", "LOUD", "LOVE", "LUCK", "LUMP", "LUNG", "LURE", "LUSH", "LUST",
      "MADE", "MAID", "MAIL", "MAIN", "MAKE", "MALE", "MALL", "MALT", "MANY", "MARE", "MARK", "MASK", "MASS", "MAST", "MATE", "MATH", "MAZE", "MEAL", "MEAN", "MEAT", "MEET", "MELT", "MEMO", "MENU", "MERE", "MESH", "MESS", "MICE", "MILD", "MILE", "MILK", "MILL", "MIME", "MIND", "MINE", "MINI", "MINT", "MISS", "MIST", "MOAT", "MOCK", "MODE", "MOLD", "MOLE", "MONK", "MOOD", "MOON", "MOOR", "MORE", "MOSS", "MOST", "MOTH", "MOVE", "MUCH", "MULE", "MUST", "MUTE", "MYTH",
      "NAIL", "NAME", "NAVE", "NEAR", "NEAT", "NECK", "NEED", "NEON", "NEST", "NEWS", "NEWT", "NEXT", "NICE", "NICK", "NINE", "NODE", "NONE", "NOOK", "NOON", "NORM", "NOSE", "NOTE", "NOUN", "NUMB", "NUTS",
      "OATH", "OATS", "OBEY", "OBOE", "ODDS", "ODOR", "OGRE", "OILY", "OINK", "OKAY", "OMEN", "OMIT", "ONCE", "ONLY", "ONTO", "OOZE", "OPEN", "ORAL", "ORCA", "ORES", "OURS", "OVAL", "OVEN", "OVER",
      "PACE", "PACK", "PACT", "PAGE", "PAID", "PAIN", "PAIR", "PALE", "PALM", "PARK", "PART", "PASS", "PAST", "PATH", "PAWN", "PEAK", "PEAR", "PEAT", "PEEK", "PEEL", "PEER", "PERK", "PEST", "PICK", "PIER", "PILE", "PILL", "PINE", "PINK", "PINT", "PIPE", "PITY", "PLAN", "PLAY", "PLEA", "PLOT", "PLOW", "PLOY", "PLUG", "PLUM", "PLUS", "POEM", "POET", "POKE", "POLE", "POLL", "POLO", "POND", "PONY", "POOL", "POOR", "PORK", "PORT", "POSE", "POSH", "POST", "POUR", "PRAY", "PREY", "PROP", "PULL", "PUMP", "PUNK", "PURE", "PUSH",
      "QUAY", "QUID", "QUIT", "QUIZ",
      "RACE", "RACK", "RAFT", "RAGE", "RAID", "RAIL", "RAIN", "RAKE", "RAMP", "RANK", "RARE", "RASH", "RATE", "READ", "REAL", "REAR", "REEF", "RELY", "RENT", "REST", "RICE", "RICH", "RIDE", "RIFT", "RING", "RIOT", "RIPE", "RISE", "RISK", "RITE", "ROAD", "ROAM", "ROAR", "ROBE", "ROCK", "RODE", "ROLE", "ROLL", "ROOF", "ROOM", "ROOT", "ROPE", "ROSE", "ROSY", "RUBY", "RUDE", "RUIN", "RULE", "RUNG", "RUSH", "RUST",
      "SACK", "SAFE", "SAGA", "SAID", "SAIL", "SAKE", "SALE", "SALT", "SAME", "SAND", "SANE", "SANG", "SANK", "SAVE", "SCAN", "SCAR", "SCUM", "SEAL", "SEAM", "SEAT", "SEED", "SEEK", "SEEM", "SEEN", "SELF", "SELL", "SEND", "SENT", "SEXY", "SHED", "SHIP", "SHOE", "SHOP", "SHOT", "SHOW", "SHUT", "SICK", "SIDE", "SIGH", "SIGN", "SILK", "SING", "SINK", "SITE", "SIZE", "SKIN", "SKIP", "SLAB", "SLAM", "SLID", "SLIM", "SLIP", "SLOT", "SLOW", "SLUM", "SMUG", "SNAP", "SNOW", "SOAP", "SOAR", "SODA", "SOFA", "SOFT", "SOIL", "SOLD", "SOLE", "SOLO", "SOME", "SONG", "SOON", "SORE", "SORT", "SOUL", "SOUP", "SOUR", "SPAN", "SPIN", "SPOT", "SPUN", "SPUR", "STAB", "STAR", "STAY", "STEM", "STEP", "STIR", "STOP", "SUCH", "SUIT", "SUNG", "SUNK", "SURE", "SWAN", "SWAP", "SWIM",
      "TACK", "TACO", "TAIL", "TAKE", "TALE", "TALK", "TALL", "TAME", "TANK", "TAPE", "TASK", "TAUT", "TAXI", "TEAL", "TEAM", "TEAR", "TELL", "TEND", "TENT", "TERM", "TEST", "TEXT", "THAN", "THAT", "THAW", "THEE", "THEM", "THEN", "THEY", "THIN", "THIS", "THOU", "THUD", "THUS", "TIDE", "TIDY", "TIED", "TIER", "TILE", "TILL", "TILT", "TIME", "TINY", "TIRE", "TOAD", "TOIL", "TOLD", "TOLL", "TOMB", "TONE", "TOOK", "TOOL", "TORE", "TORN", "TORT", "TORY", "TOSS", "TOUR", "TOWN", "TRAM", "TRAP", "TRAY", "TREE", "TRIM", "TRIO", "TRIP", "TRUE", "TSAR", "TUBE", "TUCK", "TUNA", "TUNE", "TURF", "TURN", "TWIN", "TYPE",
      "UGLY", "UNDO", "UNIT", "UNTO", "UPON", "URGE", "USED", "USER", "USES",
      "VAIN", "VARY", "VASE", "VAST", "VEIL", "VEIN", "VENT", "VERB", "VERY", "VEST", "VETO", "VIAL", "VIEW", "VILE", "VINE", "VISA", "VOID", "VOTE",
      "WADE", "WAGE", "WAIT", "WAKE", "WALK", "WALL", "WAND", "WANT", "WARD", "WARM", "WARN", "WARP", "WARY", "WASH", "WAVE", "WAVY", "WAXY", "WEAK", "WEAR", "WEED", "WEEK", "WELD", "WELL", "WENT", "WERE", "WEST", "WHAT", "WHEN", "WHIP", "WHOM", "WIDE", "WIFE", "WILD", "WILL", "WIND", "WINE", "WING", "WIPE", "WIRE", "WISE", "WISH", "WITH", "WOLF", "WOMB", "WOOD", "WOOL", "WORD", "WORE", "WORK", "WORM", "WORN", "WRAP", "WRIT",
      "XYLO",
      "YARD", "YARN", "YAWN", "YEAH", "YEAR", "YELL", "YOGA", "YOUR",
      "ZEAL", "ZERO", "ZINC", "ZONE", "ZOOM"
   };

   private string KeyboardLetters = "QWERTYUIOPASDFGHJKLZXCVBNM"; 

   void Awake () { //Avoid doing calculations in here regarding edgework. Just use this for setting up buttons for simplicity.
      moduleId = ModuleIdCounter++;
      GetComponent<KMBombModule>().OnActivate += Activate;

      foreach (KMSelectable keyPress in KeyboardButtons) {
         keyPress.OnInteract += delegate () { keyboardPress(keyPress); return false; };
      }  

      // for (int i = 0; i < 25; i++) {
      //    KeyboardButtons[i].OnInteract += delegate () { keyboardPress(i); return false; };
      //    Debug.LogFormat("Key {0} assigned to {1}", i.ToString(), KeyboardLetters[i]);
      //    //object.OnInteract += delegate () { keyPress(object); return false; };
      // }

      KeyboardBackspace.OnInteract += delegate () { backspacePress(); return false; };
      SubmitButton.OnInteract += delegate () { submitPress(); return false; };

   }

   void Start () { //Shit that you calculate, usually a majority if not all of the module
      theWord = _allWords[Rnd.Range(0, _allWords.Count())].ToLower();
      Debug.LogFormat("[Maas Cipher #{0}] The chosen English word is: {1}", moduleId, theWord);
      string charOne = "0x" + ((int)theWord[0]).ToString("X2") + ((int)theWord[1]).ToString("X2");
      string charTwo = "0x" + ((int)theWord[2]).ToString("X2") + ((int)theWord[3]).ToString("X2");
      Debug.LogFormat("[Maas Cipher #{0}] Converted to hex codes is: {1} and {2}", moduleId, charOne, charTwo);
      int convertedCharOne = Convert.ToInt32(charOne, 16);
      int convertedCharTwo = Convert.ToInt32(charTwo, 16);
      string encryptedWord = Char.ConvertFromUtf32(convertedCharOne) + Char.ConvertFromUtf32(convertedCharTwo);
      Debug.LogFormat("[Maas Cipher #{0}] The encrypted word is: {1}", moduleId, encryptedWord);

      Display.text = encryptedWord;
   }

   void keyboardPress(KMSelectable button) {
      button.AddInteractionPunch();
      //Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, button.transform);
      Audio.PlaySoundAtTransform(KeyPressSound.name, transform);
      if (!ModuleSolved) {
         if (InputDisplay.text.Length < 4) {
            char letter = button.name[3];
            InputDisplay.text += letter;
         }
      }
   }

   void backspacePress() {
      KeyboardBackspace.AddInteractionPunch();
      //Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, KeyboardBackspace.transform);
      Audio.PlaySoundAtTransform(KeyPressSound.name, transform);
      if (!ModuleSolved && InputDisplay.text.Length > 0) {
         InputDisplay.text = InputDisplay.text.Substring(0, InputDisplay.text.Length-1);
      }
   }

   void submitPress() {
      SubmitButton.AddInteractionPunch();
      if (!ModuleSolved) {
         Debug.LogFormat("[Maas Cipher #{0}] Submitted word: {1}", moduleId, InputDisplay.text);
         if (InputDisplay.text == theWord.ToUpper()) {
            Solve();
            Audio.PlaySoundAtTransform(SolveSound.name, transform);
            Debug.LogFormat("[Maas Cipher #{0}] That correctly matches the encrypted word. Module solved!", moduleId);
         } else {
            Strike();
            InputDisplay.text = "";
            Debug.LogFormat("[Maas Cipher #{0}] That does not match the encrypted word. Strike!", moduleId);
         }
      }
   }

   

   void Activate () { //Shit that should happen when the bomb arrives (factory)/Lights turn on
      InputDisplay.text = "";
   }

   void Update () { //Shit that happens at any point after initialization

   }

   void OnDestroy () { //Shit you need to do when the bomb ends
      
   }

   void Solve () {
      ModuleSolved = true;
      GetComponent<KMBombModule>().HandlePass();
   }

   void Strike () {
      GetComponent<KMBombModule>().HandleStrike();
   }

   #pragma warning disable 414
   private readonly string TwitchHelpMessage = @"Use !{0} submit <word> to submit a word.";
   #pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {
      Command = Command.Trim().ToUpper();
      yield return null;
      string[] parameters = Command.Split(' ');

      if (parameters[0] == "SUBMIT") {
         // Check submit is all letters
         for (int i = 0; i < parameters[1].Length; i++) {
            if (KeyboardLetters.IndexOf(parameters[1][i]) == -1) {
               yield return "sendtochaterror Invalid Submission.";
               yield break;
            }
         }
         // Press the keys
         for (int i = 0; i < parameters[1].Length; i++) {
            int keyPosition = KeyboardLetters.IndexOf(parameters[1][i]);
            KeyboardButtons[keyPosition].OnInteract();
         }
         yield return new WaitForSeconds(.1f);
         SubmitButton.OnInteract();
         
      } else {
         yield return "sendtochaterror Invalid Command.";
         yield break;
      }
      
   }

   IEnumerator TwitchHandleForcedSolve () {
      
      // Press the keys
         for (int i = 0; i < theWord.ToUpper().Length; i++) {
            int keyPosition = KeyboardLetters.IndexOf(theWord.ToUpper()[i]);
            KeyboardButtons[keyPosition].OnInteract();
         }
         yield return new WaitForSeconds(.1f);
         SubmitButton.OnInteract();
   }
}
