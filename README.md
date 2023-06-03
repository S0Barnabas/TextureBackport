# TextureBackport
TextureBackport can generate Beta 1.7.3 compatible versions of resource packs.  

Feature list:
- supported resource pack versions: 1.8.x - 1.19.x
- custom texture atlas templates for blocks and items
- custom resource pack resolution: 16x16 - 512x512

Custom texture atlases should be the same or lower resolution as the resource pack.  
If a lower resolution atlas is used, it will be upscaled, to the selected resoultion.

Some textures cannot be replaced on the texture atlas (see th detailed list below).  
In such cases the texture on the selected template atlas will be used instead.

List of textures that cannot be replaced:
- Chest block (single and double)
- Bed block (only if a resource pack version is higher than 1.11.x)
- Jack'o Lantern block face (only if a resource pack version is higher than 1.11.x)
- Bed item (only if a resource pack version is higher than 1.11.x)
