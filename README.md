# About TextureBackport
TextureBackport can generate Beta 1.7.3 compatible versions of resource packs.  

Feature list:
- app level support for all release versions
- default resource map supports: 1.6.x - 1.20.x
- custom resource pack resolution: 16x16 - 512x512
- XML based resource mapping allows you to fully customize the generated texture pack

These textures will not be replaced:
- Bed item (only if a resource pack version is higher than 1.11.x)
- Lava and water (planning on adding these)
- Fonts can be replaced but will not work by default due to unpredictable results at higher resolutions. This functionality can be restored by removing the comment tags from the corresponding section of the default resource map file

# Resource mapping
The resource map is a .xml file, that contains instructions as to how the resource pack should be converted to a Beta 1.7.3 compatible texture pack. 
The default resource map can be downloaded from the [releases](https://github.com/Szam0siBarnabas/TextureBackport/releases/tag/TextureBackport-v1.2.1) section. 
Resource map files must be placed into a folder named **resource_maps**, next to the application executable.

The root structure of the resource map is as follows:
```xml
<ResourceStream>
  <StreamCollection DestinationDirectory="dir/path">
    <StreamCollectionItem TemplateBitmapPath="dir/image.png">
      <BitmapStream DestinationFile="file.png">
        <SourceDirectory>src/dir/path1</SourceDirectory>
        <SourceDirectory>src/dir/path2</SourceDirectory>
        <BitmapFrame SrcPath="dir/image.png" MinVersionId="0" MaxVersionId="0">
          <TileCoords X="-1" Y="-1" />
          <SrcRect X="0" Y="0" Width="0" Height="0" />
          <DstRect X="0" Y="0" Width="0" Height="0" />
          <RotateFlipType>RotateNoneFlipNone</RotateFlipType>
        </BitmapFrame>
      </BitmapStream>
    </StreamCollectionItem>
  </StreamCollection>
</ResourceStream>
```

Changing this root structure is possible but not recommended.

# Resource map customization examples

Let's suppose that you want to port a 1.16 or newer resource pack and you want stone and cobblestone to look like deepslate and cobbled deepslate.
You can simply change the ```SrcPath``` attribute of the ```<BitmapFrame>``` tag to correspond with the deepslate and cobbled deepslate texture file name.
```xml
<ResourceStream>
  ...
  <StreamCollection DestinationDirectory="">
    <StreamCollectionItem TemplateBitmapPath="terrain.png">
      <BitmapStream DestinationFile="terrain.png">
        <SourceDirectory>assets/minecraft/textures/block</SourceDirectory>
        <SourceDirectory>assets/minecraft/textures/blocks</SourceDirectory>
        <SourceDirectory>assets/minecraft/textures/entity</SourceDirectory>
        ...
        <!--<BitmapFrame SrcPath="stone.png" MinVersionId="0" MaxVersionId="0">-->
        <BitmapFrame SrcPath="deepslate.png" MinVersionId="0" MaxVersionId="0">
          ...
        </BitmapFrame>
        <!--<BitmapFrame SrcPath="cobblestone.png" MinVersionId="0" MaxVersionId="0">-->
        <BitmapFrame SrcPath="cobbled_deepslate.png" MinVersionId="0" MaxVersionId="0">
          ...
        </BitmapFrame>
        ...
      </BitmapStream>
    </StreamCollectionItem>
  </StreamCollection>
  ...
</ResourceStream>
```

To ensure that this works starting from version 1.16, do not comment out or modify the default tags, instead create new tags and set the ```MinVersionId``` and ```MaxVersionId``` attributes as follows:
```xml
<ResourceStream>
  ...
  <StreamCollection DestinationDirectory="">
    <StreamCollectionItem TemplateBitmapPath="terrain.png">
      <BitmapStream DestinationFile="terrain.png">
        <SourceDirectory>assets/minecraft/textures/block</SourceDirectory>
        <SourceDirectory>assets/minecraft/textures/blocks</SourceDirectory>
        <SourceDirectory>assets/minecraft/textures/entity</SourceDirectory>
        ...
        <!-- Default stone texture if pack version is 1.15 or lower -->
        <BitmapFrame SrcPath="stone.png" MinVersionId="0" MaxVersionId="115">
          ...
        </BitmapFrame>
        
        <!-- Deepslate texture if pack version is 1.16 or higher -->
        <BitmapFrame SrcPath="deepslate.png" MinVersionId="116" MaxVersionId="0">
          ...
        </BitmapFrame>
        
        <!-- Default cobblestone texture if pack version is 1.15 or lower -->
        <BitmapFrame SrcPath="cobblestone.png" MinVersionId="0" MaxVersionId="115">
          ...
        </BitmapFrame>
        
        <!-- Cobbled deepslate texture if pack version is 1.16 or higher -->
        <BitmapFrame SrcPath="cobbled_deepslate.png" MinVersionId="116" MaxVersionId="0">
          ...
        </BitmapFrame>
        ...
      </BitmapStream>
    </StreamCollectionItem>
  </StreamCollection>
  ...
</ResourceStream>
```
