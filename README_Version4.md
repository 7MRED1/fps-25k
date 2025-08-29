# FPS 3D One-Click Generator (Unity + C# + Python)

هذا المستودع يوفّر مُولّد مشروع لعبة شوتر FPS ثلاثية الأبعاد لِـ Unity يعمل "دفعة واحدة". عند أي Push إلى الفرع main (أو عبر التشغيل اليدوي لخط سير العمل)، سيقوم GitHub Actions بتشغيل سكربت Python الذي يُنشئ مشروع Unity كامل داخل المستودع ويزيد إجمالي عدد الأسطر إلى 25,000+ تلقائياً.

مختصر:
- Unity: 2022 LTS (أي إصدار 2021+ يعمل غالباً).
- لغة اللعب: C#.
- المُولّد: Tools/build_fps_project.py
- الهدف: 25,000 سطر (قابل للتغيير).
- التشغيل الآلي: .github/workflows/generate-project.yml

كيفية الاستخدام محلياً (اختياري):
1) استنسخ المستودع.
2) نفّذ:
   python Tools/build_fps_project.py --target . --lines 25000
3) افتح المشروع في Unity واضغط Play على مشهد فارغ؛ الـ Bootstrap ينشئ كل شيء Runtime.

كيفية الاستخدام عبر GitHub Actions (تلقائي):
- أي Push يعدّل ملفات Tools/** أو ملف العمل generate-project.yml سيُشغّل خط سير العمل الذي:
  - يثبت Python
  - يشغّل Tools/build_fps_project.py للوصول إلى 25,000 سطر
  - يلتقط الملفات المتولّدة ويُجري Commit/Pull تلقائياً على الفرع main

تحكّم داخل اللعبة:
- WASD: حركة
- Space: قفز
- Left Shift: ركض
- Mouse: نظرة/تصويب
- زر الماوس الأيسر: إطلاق

محتوى المشروع بعد التوليد:
- Assets/Scripts/Core: Bootstrap, Game, Health, ConfigIO, JsonLite
- Assets/Scripts/Player: FPSController, MouseLook, PlayerFactory
- Assets/Scripts/Weapons: HitscanGun, AmmoState
- Assets/Scripts/AI: ChaserAI, EnemyFactory
- Assets/Scripts/Level: LevelBuilder, LevelModels
- Assets/Scripts/UI: SimpleHUD
- Assets/Scripts/Generated: مئات ملفات C# لتعبئة عدد الأسطر (لا تؤثر على اللعب)
- Assets/StreamingAssets/Configs: level1.json, weapons.json (يتم إنشاؤها تلقائياً عند الحاجة)
- Tools/map_generator.py (اختياري)

ملاحظات:
- لا حاجة إلى Prefabs؛ كل شيء يُنشأ Runtime لسهولة التشغيل.
- يمكن تعديل العدد المستهدف للأسطر عبر وسيطة --lines.
- لتجنّب حلقات تشغيل لا نهائية، خط سير العمل مقيّد بالمسارات (Tools/** وملف الـ workflow).