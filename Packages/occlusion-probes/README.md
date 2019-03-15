# Unofficial Occlusion Probes Package

This package contains the Occlusion Probes scripts for editor and runtime. Shaders or samples are not included. This package does work with both built-in RP and unmodified SRPs on Unity 2019.1+ for:
- Setting the Occlusion Probes in the editor
- Baking Occlusion Probes using Custom Bake API
- Setting the global shader parameters automatically

----------------------

To use this package (pick one):
- Clone or download this branch and place it in your projects Packages folder
- Clone or download this branch, place it anywhere you want and add folder reference to it using Package Manager
- Add direct git dependency for the package in your Packages/manifest.json:
```
occlusion-probes": "https://github.com/0lento/OcclusionProbes.git#package",
```