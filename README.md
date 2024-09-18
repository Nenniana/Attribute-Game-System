# Attribute System for Unity

This project is a flexible, extensible, and performance-optimized attribute management system for Unity, written in C#. It handles any type of numerical attributes, such as strength, health, or custom-defined properties. The system allows attributes to be modified without losing their original values, making it suitable for a variety of gameplay scenarios.

## Key Features

- **Performance first**: Attributes can be marked "dirty" and recalculated only when requested, or calculated immediately based on the project's needs.
- **Attribute Sets**: Group attributes that affect each other by drag-and-drop.
- **Custom Calculation Order**: Define the calculation sequence for attributes within a set.
- **Rounding Strategies**: Customize rounding methods per attribute type (e.g., health rounds to whole numbers, damage rounds to decimal points).
- **Event System**: Subscribe to changes in attributes with built-in events that notify of value changes.
- **Attribute System Window**: View and manage all attributes, create new ones, and define sets directly from a custom attribute inspector window.
- **Inspector Integration**: Attribute selection via a dropdown menu, avoiding the need to hard-code string references.
- **Decoupled System**: Apply attribute changes ((de)buffs, items, AOE) easily to a single entity or a whole group without tightly coupling them to the system.
  
## Detailed Overview of Features

### 1. **Base and Enhanced Attributes**
   - **Base Attributes**: Core values like health, strength, or other custom-defined stats. Developers can choose whether base attributes should be recalculated immediately or deferred until requested ("dirty" marking).
   - **Enhanced Attributes**: Modifiers that enhance base attributes in various ways:
     - **Additive**: Adds a flat value (e.g., +5 strength).
     - **Increased**: Multiplies the base value by a percentage (e.g., 50% increased strength → base * 1.5).
     - **More**: Applies a final multiplier after all other calculations (e.g., total value * 2).

### 2. **Attribute Sets**
   - **Sets**: A group of related attributes with a base attribute and its enhance modifiers. Developers can define how these attributes are calculated using custom **Calculation Groups**, which defines the order in which modifiers are applied:
     - Base calculation
     - Additive modifiers
     - Increased multipliers
     - More multipliers

### 3. **Calculation and Performance Optimization**
   - **Dirty Marking**: Attributes can be marked as "dirty" to be recalculated only upon request, optimizing performance by avoiding constant recalculations.
   - Developers can choose whether an attribute should be recalculated immediately (e.g., health) or deferred (e.g., strength).

### 4. **Attribute System Window**
   - A dedicated Unity editor window (Attribute -> Main) allows developers to view all current base and enhanced attributes. 
   - New attributes can be created through this interface. Developers can define the types of attributes to be created (e.g., Base, Additive) and whether to create an accompanying Set with a predefined calculation order.

### 5. **Inspector Integration**
   - Attributes can be exposed in the Unity Inspector using a dropdown selection instead of manually typing attribute names as strings. This reduces errors and improves usability for designers and developers.

### 6. **Buff and AoE Handling**
   - The system supports decoupling of attributes from their sources, allowing for flexible application and removal of buffs or debuffs without direct ties between entities and their modifiers.
   - Buffs can be applied across multiple entities, such as in an area-of-effect (AoE) spell.

### 7. **Rounding Strategies**
   - Each attribute can have its own rounding method. For example, health can be rounded to the nearest whole number, while damage can be rounded to the nearest decimal.
   - Developers can customize rounding rules based on the type of base attribute.

### 8. **Event System**
   - Base attributes support three types of events, allowing developers to react when values change:
     - `valueChangedToFromDiff`: Provides the new value, the previous value, and the difference between them.
     - `valueChangedToFrom`: Provides the new value and the previous value.
     - `valueChangedTo`: Provides only the new value.

## Installation

1. Clone or download this repository.
2. Add the the `Attribute System` to your script / asset folder in your Unity project.
3. Creating base-, enhance attributes, and sets in the Attribute inspector window, or use the ready-made selection.