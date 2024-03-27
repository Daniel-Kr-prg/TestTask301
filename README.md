Car tuning configurator on Unity 3D

Was made as test task for Studio 301.

List of completed tasks:
- Camera handle
- UI
- Car tuning

Extras:
- Localization
- Camera effects when selecting tuning category
- Camera idle rotating effect
- Car picture and export


How to create tuning for cars:
1) Set Car script to car's root game object. Cars are loaded from Resources/Cars folder
2) For a tuning category add TuningCategory script.
3) Create Tuning/Category asset to specify name, preview and tuning items for the category 
4) (Optional) Set camera options for the category. It will be used to affect the camera when the category is selected
5) (Optional) If the category has inner categories, add it to child categories
6) For creating tuning items create Tuning/Paint for paints and Tuning/Component for visual items assets and add them to category data.

New tuning items can be created based on TuningAppliable class. It allows to specify any kind of effect on applying and removing the tuning item.
