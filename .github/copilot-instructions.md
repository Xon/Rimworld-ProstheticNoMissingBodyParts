# Prosthetic No Missing Body Parts (Continued) - GitHub Copilot Instructions

## Mod Overview and Purpose
**Mod Name**: Prosthetic No Missing Body Parts (Continued)  
**Author**: iamrespawn (Updated continuation by Mlie)  
**PackageId**: `Mlie.ProstheticNoMissingBodyParts`

The "Prosthetic No Missing Body Parts (Continued)" mod aims to enhance the gameplay experience in RimWorld by allowing pawns with bionic arms and legs to continue wearing gear for their feet and hands, unlike the base game behavior where installing bionic limbs removes the ability to equip such gear.

## Key Features and Systems
- **Compatibility Enhancements**: Introduction of a Harmony postfix patch instead of a prefix to ensure compatibility with other mods.
- **Mod Settings**: Added mod settings to control allowed bionic replacements for hands and feet.
- **Broad Compatibility**: Works with several mod collections including vanilla, EPOE, VAE, Jewelry, AVP, but excludes mods that patch the same functionality.
- **Support for Android Tiers**: Includes support for Android Tiers and Android Tiers Reforged.
- **Realistic Modifications**: Limits certain prosthetic types from allowing gear usage based on logical reasoning (e.g., wooden hands cannot wear gloves).

## Coding Patterns and Conventions
- **Code Structure**: Organized into separate C# files for each major functionality within the mod.
  - `ApparelUtility_HasPartsToWear.cs`: Implements the Harmony postfix patch.
  - `BodyPartUtils.cs`: Contains utility functions to assist with body part checks.
  - `HarmonyPatches.cs`: Initializes Harmony patches.
  - `ProstheticNoMissingBodyPartsSettings.cs`: Manages mod settings data exposure.
  - `ProstheticNoMissingBodyPartsMod.cs`: Handles settings window and mod initialization.
- **Naming Conventions**: Follows C# naming conventions with clear, descriptive names for classes, methods, and variables.

## XML Integration
- XML files are used to define mod metadata and dependencies:
  - **About.xml**: Contains metadata about the mod.
  - **Manifest.xml**: Lists dependencies, notably `brrainz.harmony`.

## Harmony Patching
- **Postfix Patching**: Replaces destructive prefix patching with postfix patching on the `ApparelUtility.HasPartsToWear` method, ensuring results from original method execution are adjusted based on mod logic.
- **Purpose of Postfix**: Checks if bionic limbs can fulfill the role of missing body parts for wearing gear, thus influencing gameplay in a positive manner post method execution.

## Suggestions for Copilot
- **Refactoring Assistance**: Use Copilot to suggest refactoring of utility methods to increase code readability and efficiency.
- **Extending Compatibility**: Suggest additional compatibility checks or fixes with any newly released or popular mods regarding prosthetic limbs.
- **Testing Scenarios**: Generate comprehensive testing scenarios or automated tests for ensuring mod functionality under various game conditions.
- **Documentation Enhancement**: Provide suggestions for enhancing in-code comments and documentation for better clarity and future maintenance.

## Additional Notes
- **Important Load Order**: This mod should be loaded after all other mods that add bionic parts to ensure proper functionality.
- **Reliability**: While tested with over 100 other mods, users are advised to check compatibility and errors specifically when integrating with Combat Extended (CE) mods.
- **Community Support**: Users are encouraged to report any issues or provide fixes via the GitHub repository, and to seek help in dedicated Discord channels.

This instruction file serves to guide developers and contributors in understanding the structure, purpose, and potential enhancements for the "Prosthetic No Missing Body Parts (Continued)" RimWorld mod.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

