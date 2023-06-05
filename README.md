# About TextureBackport
TextureBackport can generate Beta 1.7.3 compatible versions of resource packs.  

Feature list:
- supported resource pack versions: 1.8.x - 1.19.x
- custom resource pack resolution: 16x16 - 512x512
- XML based resource mapping allows you to fully customize the generated texture pack

These textures will not be replaced:
- Bed item (only if a resource pack version is higher than 1.11.x)
- Lava and water (planning on adding these)
- Paintings (planning on adding these)

# Resource mapping
The resource map is a .xml file, that contains instructions as to how the resource pack should be converted to a Beta 1.7.3 compatible texture pack. 
The default resource map can be downloaded from the [releases](https://github.com/Szam0siBarnabas/TextureBackport/releases/tag/TextureBackport-v1.1.0) section. 
The .xml files must be placed into a folder named **resource_maps**, next to the application executable.

The root structure of the resource map is as follows:
```xml
<ResourceMap>
  <TextureAtlas sourceDirectory="block\texture\source1,block\texture\source2">
    <AtlasTexture x="0" y="0">
    </AtlasTexture>
  </TextureAtlas>
  <TextureAtlas sourceDirectory="item\texture\source1,item\texture\source2">
    <AtlasTexture x="0" y="0">
    </AtlasTexture>
  </TextureAtlas>
  <TextureBundle sourceDirectory="entity\texture\source">
    <TextureFile targetFile="file", targetWidth="0", targetHeight="0">
    </TextureFile>
  </TextureBundle>
  <TextureBundle sourceDirectory="armor\texture\source">
    <TextureFile targetFile="file", targetWidth="0", targetHeight="0">
    </TextureFile>
  </TextureBundle>
  <TextureBundle sourceDirectory="gui\texture\source">
    <TextureFile targetFile="file", targetWidth="0", targetHeight="0">
    </TextureFile>
  </TextureBundle>
</ResourceMap>
```

Changing this root structure is possible but not recommended.

# Resource map customization examples

Let's suppose that you want to port a 1.16 or newer resource pack and you want stone and cobblestone to look like deepslate and cobbled deepslate.
You can simply change the ```sourceFilename``` attribute of the ```<TextureSource>``` tag to correspond with the deepslate and cobbled deepslate texture file name (.png without extension).
```xml
<ResourceMap>
  ...
  <TextureAtlas>
    ...
    <AtlasTexture x="1" y="0">
      <!--<TextureSource sourceFileName="stone" vFrom="0" vTo="0" />-->
      <TextureSource sourceFileName="deepslate" vFrom="0" vTo="0" />
    </AtlasTexture>
    ...
    <AtlasTexture x="0" y="1">
      <!--<TextureSource sourceFileName="cobblestone" vFrom="0" vTo="0" />-->
      <TextureSource sourceFileName="cobbled_deepslate" vFrom="0" vTo="0" />
    </AtlasTexture>
    ...
  </TextureAtlas>
  ...
</ResourceMap>
```

To ensure that this works starting from version 1.16 or newer, do not comment out or modify the default tags, instead create new tags and set the ```vFrom``` and ```vTo``` attributes as follows:
```xml
<ResourceMap>
  ...
  <TextureAtlas>
    ...
    <AtlasTexture x="1" y="0">
      <TextureSource sourceFileName="stone" vFrom="0" vTo="115" />
      <TextureSource sourceFileName="deepslate" vFrom="116" vTo="0" />
    </AtlasTexture>
    ...
    <AtlasTexture x="0" y="1">
      <TextureSource sourceFileName="cobblestone" vFrom="0" vTo="115" />
      <TextureSource sourceFileName="cobbled_deepslate" vFrom="116" vTo="0" />
    </AtlasTexture>
    ...
  </TextureAtlas>
  ...
</ResourceMap>
```
