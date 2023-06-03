using System.Drawing;
using System.IO.Compression;

namespace TextureBackport.Api.Textures;

public delegate void PortFinished();

public static class Backport
{
    private static string textureFolder = @"texture_source\assets\minecraft\textures";
    private static string blocksFolder;
    private static string itemsFolder;
    private static string entityFolder;
    private static string armorFolder;

    public static TextureAtlas terrain { get; private set; }
    public static TextureAtlas items { get; private set; }
    public static TextureBundle entity { get; private set; }
    public static TextureBundle armor { get; private set; }

    public static Bitmap TerrainTemplate;
    public static Bitmap ItemTemplate;

    public static event PortFinished OnPortFinished;

    public static async void CreateTexturePack(
        Version version,
        TextureResolution resolution,
        string sourceFile,
        string targetDirectory,
        string? terrainTemplateAtlas,
        string? itemTemplateAtlas)
    {
        if (Directory.Exists("texture_source"))
            Directory.Delete("texture_source", true);
        if (Directory.Exists("backport"))
            Directory.Delete("backport", true);

        ZipFile.ExtractToDirectory(sourceFile, "texture_source");
        Directory.CreateDirectory("backport");

        blocksFolder = Path.Combine(textureFolder, "block" + (version < Version.V113X ? "s" : ""));
        itemsFolder = Path.Combine(textureFolder, "item" + (version < Version.V113X ? "s" : ""));
        entityFolder = Path.Combine(textureFolder, "entity");
        armorFolder = Path.Combine(textureFolder, @"models\armor");

        #region TERRAIN

        terrain = new TextureAtlas(resolution);
        terrain.AddTexture(0, 0).FromTo("grass_top", vTo: Version.V112X).FromTo("grass_block_top", Version.V113X);
        terrain.AddTexture(0, 1).FromTo("stone");
        terrain.AddTexture(0, 2).FromTo("dirt");
        terrain.AddTexture(0, 3).FromTo("grass_side", vTo: Version.V112X).FromTo("grass_block_side", Version.V113X);
        terrain.AddTexture(0, 4).FromTo("planks_oak", vTo: Version.V112X).FromTo("oak_planks", Version.V113X);
        terrain.AddTexture(0, 5).FromTo("stone_slab_side", vTo: Version.V113X)
            .FromTo("smooth_stone_slab_side", Version.V114X);
        terrain.AddTexture(0, 6).FromTo("stone_slab_top", vTo: Version.V113X).FromTo("smooth_stone", Version.V114X);
        terrain.AddTexture(0, 7).FromTo("brick", vTo: Version.V112X).FromTo("bricks", Version.V113X);
        terrain.AddTexture(0, 8).FromTo("tnt_side");
        terrain.AddTexture(0, 9).FromTo("tnt_top");
        terrain.AddTexture(0, 10).FromTo("tnt_bottom");
        terrain.AddTexture(0, 11).FromTo("web", vTo: Version.V112X).FromTo("cobweb", Version.V113X);
        terrain.AddTexture(0, 12).FromTo("flower_rose", vTo: Version.V112X).FromTo("poppy", Version.V113X);
        terrain.AddTexture(0, 13).FromTo("flower_dandelion", vTo: Version.V112X).FromTo("dandelion", Version.V113X);
        terrain.AddTexture(0, 15).FromTo("sapling_oak", vTo: Version.V112X).FromTo("oak_sapling", Version.V113X);
        terrain.AddTexture(1, 0).FromTo("cobblestone");
        terrain.AddTexture(1, 1).FromTo("bedrock");
        terrain.AddTexture(1, 2).FromTo("sand");
        terrain.AddTexture(1, 3).FromTo("gravel");
        terrain.AddTexture(1, 4).FromTo("log_oak", vTo: Version.V112X).FromTo("oak_log", Version.V113X);
        terrain.AddTexture(1, 5).FromTo("log_oak_top", vTo: Version.V112X).FromTo("oak_log_top", Version.V113X);
        terrain.AddTexture(1, 6).FromTo("iron_block");
        terrain.AddTexture(1, 7).FromTo("gold_block");
        terrain.AddTexture(1, 8).FromTo("diamond_block");
        terrain.AddComposite(1, 9, entityFolder, (src, res, ver) =>
        {
            var resMul = (int)res / 16;
            var result = new Bitmap((int)res, (int)res);
            using var sourceBmp = (Bitmap)Image.FromFile(src);
            using var g = Graphics.FromImage(result);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            var chestTopX = ver > Version.V114X ? 28 : 14;

            using var chestTop = sourceBmp.Clone(new Rectangle(chestTopX * resMul, 0, 14 * resMul, 14 * resMul), sourceBmp.PixelFormat);
            g.DrawImage(chestTop, 0, 0, (int)res, (int)res);

            return result;
        }).FromTo(@"chest\normal");
        terrain.AddComposite(1, 10, entityFolder, (src, res, ver) => 
        {
            var resMul = (int)res / 16;
            var result = new Bitmap((int)res, (int)res);
            using var sourceBmp = (Bitmap)Image.FromFile(src);
            using var g = Graphics.FromImage(result);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            var chestSideBottomY = ver > Version.V114X ? 33 : 34;

            var chestSideTop = sourceBmp.Clone(new Rectangle(0, 14 * resMul, 14 * resMul, 5 * resMul), sourceBmp.PixelFormat);
            var chestSideBottom = sourceBmp.Clone(new Rectangle(0, chestSideBottomY * resMul, 14 * resMul, 9 * resMul), sourceBmp.PixelFormat);
            if (ver > Version.V114X)
            {
                chestSideTop.RotateFlip(RotateFlipType.RotateNoneFlipY);
                chestSideBottom.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            g.DrawImage(chestSideTop, 0, 0, 16 * resMul, 6 * resMul);
            g.DrawImage(chestSideBottom, 0, 6 * resMul, 16 * resMul, 10 * resMul);

            return result;
        }).FromTo(@"chest\normal");
        terrain.AddComposite(1, 11, entityFolder, (src, res, ver) => 
        {
            var resMul = (int)res / 16;
            var result = new Bitmap((int)res, (int)res);
            using var sourceBmp = (Bitmap)Image.FromFile(src);
            using var g = Graphics.FromImage(result);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            var chestFrontX = ver > Version.V114X ? 42 : 14;
            var chestFrontBottomY = ver > Version.V114X ? 33 : 34;

            using var chestFrontTop = sourceBmp.Clone(new Rectangle(chestFrontX * resMul, 14 * resMul, 14 * resMul, 5 * resMul), sourceBmp.PixelFormat);
            using var chestFrontBottom = sourceBmp.Clone(new Rectangle(chestFrontX * resMul, chestFrontBottomY * resMul, 14 * resMul, 9 * resMul), sourceBmp.PixelFormat);
            using var chestFrontLock = sourceBmp.Clone(new Rectangle(1 * resMul, 1 * resMul, 2 * resMul, 4 * resMul), sourceBmp.PixelFormat);
            if (ver > Version.V114X)
            {
                chestFrontTop.RotateFlip(RotateFlipType.RotateNoneFlipY);
                chestFrontBottom.RotateFlip(RotateFlipType.RotateNoneFlipY);
                chestFrontLock.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            g.DrawImage(chestFrontTop, 0, 0, 16 * resMul, 6 * resMul);
            g.DrawImage(chestFrontBottom, 0, 6 * resMul, 16 * resMul, 10 * resMul);
            g.DrawImage(chestFrontLock, 7 * resMul, 3 * resMul, 2 * resMul, 4 * resMul);

            return result;
        }).FromTo(@"chest\normal");
        terrain.AddTexture(1, 12).FromTo("mushroom_red", vTo: Version.V112X).FromTo("red_mushroom", Version.V113X);
        terrain.AddTexture(1, 13).FromTo("mushroom_brown", vTo: Version.V112X).FromTo("brown_mushroom", Version.V113X);
        terrain.AddTexture(2, 1).FromTo("gold_ore");
        terrain.AddTexture(2, 0).FromTo("iron_ore");
        terrain.AddTexture(2, 2).FromTo("coal_ore");
        terrain.AddTexture(2, 3).FromTo("bookshelf");
        terrain.AddTexture(2, 4).FromTo("cobblestone_mossy", vTo: Version.V112X)
            .FromTo("mossy_cobblestone", Version.V113X);
        terrain.AddTexture(2, 5).FromTo("obsidian");
        terrain.AddTexture(2, 6).FromTo("grass_side_overlay", vTo: Version.V112X)
            .FromTo("grass_block_side_overlay", Version.V113X);
        terrain.AddTexture(2, 7).FromTo("tallgrass", vTo: Version.V112X).FromTo("grass", Version.V113X);
        terrain.AddTexture(2, 8).FromTo("grass_top", vTo: Version.V112X).FromTo("grass_block_top", Version.V113X);
        terrain.AddComposite(2, 9, entityFolder, (src, res, ver) => {
            var resMul = (int)res / 16;
            var result = new Bitmap((int)res, (int)res);
            using var sourceBmp = (Bitmap)Image.FromFile(src);
            using var g = Graphics.FromImage(result);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            var chestLeftFrontX = ver > Version.V114X ? 43 : 14;
            var chestLeftFrontBottomY = ver > Version.V114X ? 33 : 34;
            var chestLeftFrontLockX = ver > Version.V114X ? 3 : 1;
            using var doubleChestLeftFrontTop = sourceBmp.Clone(new Rectangle(chestLeftFrontX * resMul, 14 * resMul, 15 * resMul, 5 * resMul), sourceBmp.PixelFormat);
            using var doubleChestLeftFrontBottom = sourceBmp.Clone(new Rectangle(chestLeftFrontX * resMul, chestLeftFrontBottomY * resMul, 15 * resMul, 9 * resMul), sourceBmp.PixelFormat);
            using var doubleChestLeftFrontLock = sourceBmp.Clone(new Rectangle(chestLeftFrontLockX * resMul, 1 * resMul, 1 * resMul, 4 * resMul), sourceBmp.PixelFormat);
            if (ver > Version.V114X)
            {
                doubleChestLeftFrontTop.RotateFlip(RotateFlipType.RotateNoneFlipY);
                doubleChestLeftFrontBottom.RotateFlip(RotateFlipType.RotateNoneFlipY);
                doubleChestLeftFrontLock.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            g.DrawImage(doubleChestLeftFrontTop, 0, 0, 16 * resMul, 6 * resMul);
            g.DrawImage(doubleChestLeftFrontBottom, 0, 6 * resMul, 16 * resMul, 10 * resMul);
            g.DrawImage(doubleChestLeftFrontLock, 15 * resMul, 3 * resMul, 1 * resMul, 4 * resMul);

            return result;
        }).FromTo(@"chest\normal_double", vTo: Version.V114X).FromTo(@"chest\normal_left", Version.V115X);
        terrain.AddComposite(2, 10, entityFolder, (src, res, ver) => {
            var resMul = (int)res / 16;
            var result = new Bitmap((int)res, (int)res);
            using var sourceBmp = (Bitmap)Image.FromFile(src);
            using var g = Graphics.FromImage(result);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            var chestRightFrontX = ver > Version.V114X ? 43 : 29;
            var chestRightFrontBottomY = ver > Version.V114X ? 33 : 34;
            var chestRightFrontLockX = ver > Version.V114X ? 3 : 2;
            using var doubleChestRightFrontTop = sourceBmp.Clone(new Rectangle(chestRightFrontX * resMul, 14 * resMul, 15 * resMul, 5 * resMul), sourceBmp.PixelFormat);
            using var doubleChestRightFrontBottom = sourceBmp.Clone(new Rectangle(chestRightFrontX * resMul, chestRightFrontBottomY * resMul, 15 * resMul, 9 * resMul), sourceBmp.PixelFormat);
            using var doubleChestRightFrontLock = sourceBmp.Clone(new Rectangle(chestRightFrontLockX * resMul, 1 * resMul, 1 * resMul, 4 * resMul), sourceBmp.PixelFormat);
            if (ver > Version.V114X)
            {
                doubleChestRightFrontTop.RotateFlip(RotateFlipType.RotateNoneFlipY);
                doubleChestRightFrontBottom.RotateFlip(RotateFlipType.RotateNoneFlipY);
                doubleChestRightFrontLock.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            g.DrawImage(doubleChestRightFrontTop, 0, 0, 16 * resMul, 6 * resMul);
            g.DrawImage(doubleChestRightFrontBottom, 0, 6 * resMul, 16 * resMul, 10 * resMul);
            g.DrawImage(doubleChestRightFrontLock, 0, 3 * resMul, 1 * resMul, 4 * resMul);

            return result;
        }).FromTo(@"chest\normal_double", vTo: Version.V114X).FromTo(@"chest\normal_right", Version.V115X);
        terrain.AddTexture(2, 11).FromTo("crafting_table_top");
        terrain.AddTexture(2, 12).FromTo("furnace_front_off", vTo: Version.V112X)
            .FromTo("furnace_front", Version.V113X);
        terrain.AddTexture(2, 13).FromTo("furnace_side");
        terrain.AddTexture(2, 14).FromTo("dispenser_front_horizontal", vTo: Version.V112X)
            .FromTo("dispenser_front", Version.V113X);
        terrain.AddTexture(3, 0).FromTo("sponge");
        terrain.AddTexture(3, 1).FromTo("glass");
        terrain.AddTexture(3, 2).FromTo("diamond_ore");
        terrain.AddTexture(3, 3).FromTo("redstone_ore");
        terrain.AddTexture(3, 4).FromTo("leaves_birch", vTo: Version.V112X).FromTo("birch_leaves", Version.V113X);
        terrain.AddTexture(3, 5).FromTo("leaves_oak", vTo: Version.V112X).FromTo("oak_leaves", Version.V113X);
        terrain.AddTexture(3, 7).FromTo("deadbush", vTo: Version.V112X).FromTo("dead_bush", Version.V113X);
        terrain.AddTexture(3, 8).FromTo("fern");
        terrain.AddComposite(3, 9, entityFolder, (src, res, ver) => {
            var resMul = (int)res / 16;
            var result = new Bitmap((int)res, (int)res);
            using var sourceBmp = (Bitmap)Image.FromFile(src);
            using var g = Graphics.FromImage(result);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            var chestRightBackX = ver > Version.V114X ? 14 : 58;
            var chestRightBackBottomY = ver > Version.V114X ? 33 : 34;
            using var doubleChestRightBackTop = sourceBmp.Clone(new Rectangle(chestRightBackX * resMul, 14 * resMul, 15 * resMul, 5 * resMul), sourceBmp.PixelFormat);
            using var doubleChestRightBackBottom = sourceBmp.Clone(new Rectangle(chestRightBackX * resMul, chestRightBackBottomY * resMul, 15 * resMul, 9 * resMul), sourceBmp.PixelFormat);
            if (ver > Version.V114X)
            {
                doubleChestRightBackTop.RotateFlip(RotateFlipType.RotateNoneFlipY);
                doubleChestRightBackBottom.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            g.DrawImage(doubleChestRightBackTop, 0, 0, 16 * resMul, 6 * resMul);
            g.DrawImage(doubleChestRightBackBottom, 0, 6 * resMul, 16 * resMul, 10 * resMul);

            return result;
        }).FromTo(@"chest\normal_double", vTo: Version.V114X).FromTo(@"chest\normal_right", Version.V115X);
        terrain.AddComposite(3, 10, entityFolder, (src, res, ver) => {
            var resMul = (int)res / 16;
            var result = new Bitmap((int)res, (int)res);
            using var sourceBmp = (Bitmap)Image.FromFile(src);
            using var g = Graphics.FromImage(result);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            var chestLeftBackX = ver > Version.V114X ? 14 : 73;
            var chestLeftBackBottomY = ver > Version.V114X ? 33 : 34;
            using var doubleChestLeftBackTop = sourceBmp.Clone(new Rectangle(chestLeftBackX * resMul, 14 * resMul, 15 * resMul, 5 * resMul), sourceBmp.PixelFormat);
            using var doubleChestLeftBackBottom = sourceBmp.Clone(new Rectangle(chestLeftBackX * resMul, chestLeftBackBottomY * resMul, 15 * resMul, 9 * resMul), sourceBmp.PixelFormat);
            if (ver > Version.V114X)
            {
                doubleChestLeftBackTop.RotateFlip(RotateFlipType.RotateNoneFlipY);
                doubleChestLeftBackBottom.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            g.DrawImage(doubleChestLeftBackTop, 0, 0, 16 * resMul, 6 * resMul);
            g.DrawImage(doubleChestLeftBackBottom, 0, 6 * resMul, 16 * resMul, 10 * resMul);

            return result;
        }).FromTo(@"chest\normal_double", vTo: Version.V114X).FromTo(@"chest\normal_left", Version.V115X);
        terrain.AddTexture(3, 11).FromTo("crafting_table_side");
        terrain.AddTexture(3, 12).FromTo("crafting_table_front");
        terrain.AddTexture(3, 13).FromTo("furnace_front_on");
        terrain.AddTexture(3, 14).FromTo("furnace_top");
        terrain.AddTexture(3, 15).FromTo("sapling_spruce", vTo: Version.V112X).FromTo("spruce_sapling", Version.V113X);
        terrain.AddTexture(4, 0).FromTo("wool_colored_white", vTo: Version.V112X).FromTo("white_wool", Version.V113X);
        terrain.AddTexture(4, 1).FromTo("mob_spawner", vTo: Version.V112X).FromTo("spawner", Version.V113X);
        terrain.AddTexture(4, 2).FromTo("snow");
        terrain.AddTexture(4, 3).FromTo("ice");
        terrain.AddTexture(4, 4).FromTo("grass_side_snowed", vTo: Version.V112X)
            .FromTo("grass_block_snow", Version.V113X);
        terrain.AddTexture(4, 5).FromTo("cactus_top");
        terrain.AddTexture(4, 6).FromTo("cactus_side");
        terrain.AddTexture(4, 7).FromTo("cactus_bottom");
        terrain.AddTexture(4, 8).FromTo("clay");
        terrain.AddTexture(4, 9).FromTo("reeds", vTo: Version.V112X).FromTo("sugar_cane", Version.V113X);
        terrain.AddTexture(4, 10).FromTo("jukebox_side");
        terrain.AddTexture(4, 11).FromTo("jukebox_top");
        terrain.AddTexture(4, 15).FromTo("sapling_birch", vTo: Version.V112X).FromTo("birch_sapling", Version.V113X);
        terrain.AddTexture(5, 0).FromTo("torch_on", vTo: Version.V112X).FromTo("torch", Version.V113X);
        terrain.AddTexture(5, 1).FromTo("door_wood_upper", vTo: Version.V112X).FromTo("oak_door_top", Version.V113X);
        terrain.AddTexture(5, 2).FromTo("door_iron_upper", vTo: Version.V112X).FromTo("iron_door_top", Version.V113X);
        terrain.AddTexture(5, 3).FromTo("ladder");
        terrain.AddTexture(5, 4).FromTo("trapdoor", vTo: Version.V112X).FromTo("oak_trapdoor", Version.V113X);
        terrain.AddTexture(5, 6).FromTo("farmland_wet", vTo: Version.V112X).FromTo("farmland_moist", Version.V113X);
        terrain.AddTexture(5, 7).FromTo("farmland_dry", vTo: Version.V112X).FromTo("farmland", Version.V113X);
        terrain.AddTexture(5, 8).FromTo("wheat_stage_0", vTo: Version.V112X).FromTo("wheat_stage0", Version.V113X);
        terrain.AddTexture(5, 9).FromTo("wheat_stage_1", vTo: Version.V112X).FromTo("wheat_stage1", Version.V113X);
        terrain.AddTexture(5, 10).FromTo("wheat_stage_2", vTo: Version.V112X).FromTo("wheat_stage2", Version.V113X);
        terrain.AddTexture(5, 11).FromTo("wheat_stage_3", vTo: Version.V112X).FromTo("wheat_stage3", Version.V113X);
        terrain.AddTexture(5, 12).FromTo("wheat_stage_4", vTo: Version.V112X).FromTo("wheat_stage4", Version.V113X);
        terrain.AddTexture(5, 13).FromTo("wheat_stage_5", vTo: Version.V112X).FromTo("wheat_stage5", Version.V113X);
        terrain.AddTexture(5, 14).FromTo("wheat_stage_6", vTo: Version.V112X).FromTo("wheat_stage6", Version.V113X);
        terrain.AddTexture(5, 15).FromTo("wheat_stage_7", vTo: Version.V112X).FromTo("wheat_stage7", Version.V113X);
        terrain.AddTexture(6, 0).FromTo("lever");
        terrain.AddTexture(6, 1).FromTo("door_wood_lower", vTo: Version.V112X).FromTo("oak_door_bottom", Version.V113X);
        terrain.AddTexture(6, 2).FromTo("door_iron_lower", vTo: Version.V112X)
            .FromTo("iron_door_bottom", Version.V113X);
        terrain.AddTexture(6, 3).FromTo("redstone_torch_on", vTo: Version.V112X)
            .FromTo("redstone_torch", Version.V113X);
        terrain.AddTexture(6, 6).FromTo("pumpkin_top");
        terrain.AddTexture(6, 7).FromTo("netherrack");
        terrain.AddTexture(6, 8).FromTo("soul_sand");
        terrain.AddTexture(6, 9).FromTo("glowstone");
        terrain.AddTexture(6, 10).FromTo("piston_top_sticky");
        terrain.AddTexture(6, 11).FromTo("piston_top_normal", vTo: Version.V112X).FromTo("piston_top", Version.V113X);
        terrain.AddTexture(6, 12).FromTo("piston_side");
        terrain.AddTexture(6, 13).FromTo("piston_bottom");
        terrain.AddTexture(6, 14).FromTo("piston_inner");
        terrain.AddTexture(7, 0).FromTo("rail_normal_turned", vTo: Version.V112X).FromTo("rail_corner", Version.V113X);
        terrain.AddTexture(7, 1).FromTo("wool_colored_black", vTo: Version.V112X).FromTo("black_wool", Version.V113X);
        terrain.AddTexture(7, 2).FromTo("wool_colored_gray", vTo: Version.V112X).FromTo("gray_wool", Version.V113X);
        terrain.AddTexture(7, 3).FromTo("redstone_torch_off");
        terrain.AddTexture(7, 4).FromTo("log_spruce", vTo: Version.V112X).FromTo("spruce_log", Version.V113X);
        terrain.AddTexture(7, 5).FromTo("log_birch", vTo: Version.V112X).FromTo("birch_log", Version.V113X);
        terrain.AddTexture(7, 6).FromTo("pumpkin_side");
        terrain.AddTexture(7, 7).FromTo("pumpkin_face_off", vTo: Version.V112X).FromTo("carved_pumpkin", Version.V113X);
        terrain.AddTexture(7, 8).FromTo("pumpkin_face_on", vTo: Version.V111X);
        terrain.AddTexture(7, 9).FromTo("cake_top");
        terrain.AddTexture(7, 10).FromTo("cake_side");
        terrain.AddTexture(7, 11).FromTo("cake_inner");
        terrain.AddTexture(7, 12).FromTo("cake_bottom");
        terrain.AddTexture(8, 0).FromTo("rail_normal", vTo: Version.V112X).FromTo("rail", Version.V113X);
        terrain.AddTexture(8, 1).FromTo("wool_colored_red", vTo: Version.V112X).FromTo("red_wool", Version.V113X);
        terrain.AddTexture(8, 2).FromTo("wool_colored_pink", vTo: Version.V112X).FromTo("pink_wool", Version.V113X);
        terrain.AddTexture(8, 3).FromTo("repeater_off", vTo: Version.V112X).FromTo("repeater", Version.V113X);
        terrain.AddTexture(8, 4).FromTo("leaves_birch", vTo: Version.V112X).FromTo("birch_leaves", Version.V113X);
        terrain.AddTexture(8, 5).FromTo("leaves_spruce", vTo: Version.V112X).FromTo("spruce_leaves", Version.V113X);
        terrain.AddTexture(8, 6).FromTo("bed_feet_top", vTo: Version.V111X);
        terrain.AddTexture(8, 7).FromTo("bed_head_top", vTo: Version.V111X);
        terrain.AddTexture(9, 0).FromTo("lapis_block");
        terrain.AddTexture(9, 1).FromTo("wool_colored_green", vTo: Version.V112X).FromTo("green_wool", Version.V113X);
        terrain.AddTexture(9, 2).FromTo("wool_colored_lime", vTo: Version.V112X).FromTo("lime_wool", Version.V113X);
        terrain.AddTexture(9, 3).FromTo("repeater_on");
        terrain.AddTexture(9, 5).FromTo("bed_feet_end", vTo: Version.V111X);
        terrain.AddTexture(9, 6).FromTo("bed_feet_side", vTo: Version.V111X);
        terrain.AddTexture(9, 7).FromTo("bed_head_side", vTo: Version.V111X);
        terrain.AddTexture(9, 8).FromTo("bed_head_end", vTo: Version.V111X);
        terrain.AddTexture(10, 0).FromTo("lapis_ore");
        terrain.AddTexture(10, 1).FromTo("wool_colored_brown", vTo: Version.V112X).FromTo("brown_wool", Version.V113X);
        terrain.AddTexture(10, 2).FromTo("wool_colored_yellow", vTo: Version.V112X)
            .FromTo("yellow_wool", Version.V113X);
        terrain.AddTexture(10, 3).FromTo("rail_golden", vTo: Version.V112X).FromTo("powered_rail", Version.V113X);
        terrain.AddTexture(10, 4).FromTo("redstone_dust_cross", vTo: Version.V18X)
            .FromTo("redstone_dust_dot", Version.V19X);
        terrain.AddTexture(10, 5).FromTo("redstone_dust_line", vTo: Version.V18X)
            .FromTo("redstone_dust_line0", Version.V19X);
        terrain.AddTexture(11, 0).FromTo("sandstone_top");
        terrain.AddTexture(11, 1).FromTo("wool_colored_blue", vTo: Version.V112X).FromTo("blue_wool", Version.V113X);
        terrain.AddTexture(11, 2).FromTo("wool_colored_light_blue", vTo: Version.V112X)
            .FromTo("light_blue_wool", Version.V113X);
        terrain.AddTexture(11, 3).FromTo("rail_golden_powered", vTo: Version.V112X)
            .FromTo("powered_rail_on", Version.V113X);
        terrain.AddTexture(12, 0).FromTo("sandstone_noraml", vTo: Version.V112X).FromTo("sandstone", Version.V113X);
        terrain.AddTexture(12, 1).FromTo("wool_colored_purple", vTo: Version.V112X)
            .FromTo("purple_wool", Version.V113X);
        terrain.AddTexture(12, 2).FromTo("wool_colored_magenta", vTo: Version.V112X)
            .FromTo("magenta_wool", Version.V113X);
        terrain.AddTexture(12, 3).FromTo("rail_detector", vTo: Version.V112X).FromTo("detector_rail", Version.V113X);
        terrain.AddTexture(13, 0).FromTo("sandstone_bottom");
        terrain.AddTexture(13, 1).FromTo("wool_colored_cyan", vTo: Version.V112X).FromTo("cyan_wool", Version.V113X);
        terrain.AddTexture(13, 2).FromTo("wool_colored_orange", vTo: Version.V112X)
            .FromTo("orange_wool", Version.V113X);
        terrain.AddTexture(14, 1).FromTo("wool_colored_silver", vTo: Version.V112X)
            .FromTo("light_gray_wool", Version.V113X);
        terrain.AddTexture(15, 0).FromTo("destroy_stage_0");
        terrain.AddTexture(15, 1).FromTo("destroy_stage_1");
        terrain.AddTexture(15, 2).FromTo("destroy_stage_2");
        terrain.AddTexture(15, 3).FromTo("destroy_stage_3");
        terrain.AddTexture(15, 4).FromTo("destroy_stage_4");
        terrain.AddTexture(15, 5).FromTo("destroy_stage_5");
        terrain.AddTexture(15, 6).FromTo("destroy_stage_6");
        terrain.AddTexture(15, 7).FromTo("destroy_stage_7");
        terrain.AddTexture(15, 8).FromTo("destroy_stage_8");
        terrain.AddTexture(15, 9).FromTo("destroy_stage_9");
        #endregion

        #region ITEMS
        items = new TextureAtlas(resolution);
        items.AddTexture(0, 1).FromTo("chainmail_helmet");
        items.AddTexture(0, 2).FromTo("iron_helmet");
        items.AddTexture(0, 3).FromTo("diamond_helmet");
        items.AddTexture(0, 4).FromTo("gold_helmet", vTo: Version.V112X).FromTo("golden_helmet", Version.V113X);
        items.AddTexture(0, 5).FromTo("flint_and_steel");
        items.AddTexture(0, 6).FromTo("flint");
        items.AddTexture(0, 7).FromTo("coal");
        items.AddTexture(0, 8).FromTo("string");
        items.AddTexture(0, 9).FromTo("seeds_wheat", vTo: Version.V112X).FromTo("wheat_seeds", Version.V113X);
        items.AddTexture(0, 10).FromTo("apple");
        items.AddTexture(0, 11).FromTo("apple_golden", vTo: Version.V112X).FromTo("golden_apple", Version.V113X);
        items.AddTexture(0, 12).FromTo("egg");
        items.AddTexture(0, 13).FromTo("sugar");
        items.AddTexture(0, 14).FromTo("snowball");
        items.AddTexture(0, 15).FromTo("empty_armor_slot_helmet");
        items.AddTexture(1, 1).FromTo("chainmail_chestplate");
        items.AddTexture(1, 2).FromTo("iron_chestplate");
        items.AddTexture(1, 3).FromTo("diamond_chestplate");
        items.AddTexture(1, 4).FromTo("gold_chestplate", vTo: Version.V112X).FromTo("golden_chestplate", Version.V113X);
        items.AddTexture(1, 5).FromTo("bow_standby", vTo: Version.V112X).FromTo("bow", Version.V113X);
        items.AddTexture(1, 6).FromTo("brick");
        items.AddTexture(1, 7).FromTo("iron_ingot");
        items.AddTexture(1, 8).FromTo("feather");
        items.AddTexture(1, 9).FromTo("wheat");
        items.AddTexture(1, 10).FromTo("painting");
        items.AddTexture(1, 11).FromTo("reeds", vTo: Version.V112X).FromTo("sugar_cane", Version.V113X);
        items.AddTexture(1, 12).FromTo("bone");
        items.AddTexture(1, 13).FromTo("cake");
        items.AddTexture(1, 14).FromTo("slimebal", vTo: Version.V112X).FromTo("slime_ball", Version.V113X);
        items.AddTexture(1, 15).FromTo("empty_armor_slot_chestplate");
        items.AddTexture(2, 1).FromTo("chainmail_leggings");
        items.AddTexture(2, 2).FromTo("iron_leggings");
        items.AddTexture(2, 3).FromTo("diamond_leggings");
        items.AddTexture(2, 4).FromTo("gold_leggings", vTo: Version.V112X).FromTo("golden_leggings", Version.V113X);
        items.AddTexture(2, 5).FromTo("arrow");
        items.AddTexture(2, 7).FromTo("gold_ingot");
        items.AddTexture(2, 8).FromTo("gunpowder");
        items.AddTexture(2, 9).FromTo("bread");
        items.AddTexture(2, 10).FromTo("sign", vTo: Version.V112X).FromTo("oak_sign", Version.V113X);
        items.AddTexture(2, 11).FromTo("door_wood", vTo: Version.V112X).FromTo("oak_door", Version.V113X);
        items.AddTexture(2, 12).FromTo("door_iron", vTo: Version.V112X).FromTo("iron_door", Version.V113X);
        items.AddTexture(2, 13).FromTo("bed", vTo: Version.V112X);
        items.AddTexture(2, 15).FromTo("empty_armor_slot_leggings");
        items.AddTexture(3, 1).FromTo("chainmail_boots");
        items.AddTexture(3, 2).FromTo("iron_boots");
        items.AddTexture(3, 3).FromTo("diamond_boots");
        items.AddTexture(3, 4).FromTo("gold_boots", vTo: Version.V112X).FromTo("golden_boots", Version.V113X);
        items.AddTexture(3, 5).FromTo("stick");
        items.AddTexture(3, 7).FromTo("diamond");
        items.AddTexture(3, 8).FromTo("redstone_dust", vTo: Version.V112X).FromTo("redstone", Version.V113X);
        items.AddTexture(3, 9).FromTo("clay_ball");
        items.AddTexture(3, 10).FromTo("paper");
        items.AddTexture(3, 11).FromTo("book_noraml", vTo: Version.V112X).FromTo("book", Version.V113X);
        items.AddTexture(3, 12).FromTo("map_empty", vTo: Version.V112X).FromTo("map", Version.V113X);
        items.AddTexture(3, 15).FromTo("empty_armor_slot_boots");
        items.AddTexture(4, 0).FromTo("wood_sword", vTo: Version.V112X).FromTo("wooden_sword", Version.V113X);
        items.AddTexture(4, 1).FromTo("stone_sword");
        items.AddTexture(4, 2).FromTo("iron_sword");
        items.AddTexture(4, 3).FromTo("gold_sword", vTo: Version.V112X).FromTo("golden_sword", Version.V113X);
        items.AddTexture(4, 4).FromTo("diamond_sword");
        items.AddTexture(4, 5).FromTo("fishing_rod_uncast", vTo: Version.V112X).FromTo("fishing_rod", Version.V113X);
        items.AddTexture(4, 7).FromTo("bowl");
        items.AddTexture(4, 8).FromTo("mushroom_stew");
        items.AddTexture(4, 9).FromTo("glowstone_dust");
        items.AddTexture(4, 10).FromTo("bucket_empty", vTo: Version.V112X).FromTo("bucket", Version.V113X);
        items.AddTexture(4, 11).FromTo("bucket_water", vTo: Version.V112X).FromTo("water_bucket", Version.V113X);
        items.AddTexture(4, 12).FromTo("bucket_lava", vTo: Version.V112X).FromTo("lava_bucket", Version.V113X);
        items.AddTexture(4, 13).FromTo("bucket_milk", vTo: Version.V112X).FromTo("milk_bucket", Version.V113X);
        items.AddTexture(4, 14).FromTo("dye_powder_black", vTo: Version.V112X).FromTo("ink_sac", Version.V113X);
        items.AddTexture(4, 15).FromTo("dye_powder_gray", vTo: Version.V112X).FromTo("gray_dye", Version.V113X);
        items.AddTexture(5, 0).FromTo("wood_shovel", vTo: Version.V112X).FromTo("wooden_shovel", Version.V113X);
        items.AddTexture(5, 1).FromTo("stone_shovel");
        items.AddTexture(5, 2).FromTo("iron_shovel");
        items.AddTexture(5, 3).FromTo("gold_shovel", vTo: Version.V112X).FromTo("golden_shovel", Version.V113X);
        items.AddTexture(5, 4).FromTo("diamond_shovel");
        items.AddTexture(5, 5).FromTo("fishing_rod_cast");
        items.AddTexture(5, 6).FromTo("repeater");
        items.AddTexture(5, 7).FromTo("porkchop_raw", vTo: Version.V112X).FromTo("porkchop", Version.V113X);
        items.AddTexture(5, 8).FromTo("porkchop_cooked", vTo: Version.V112X).FromTo("cooked_porkchop", Version.V113X);
        items.AddTexture(5, 9).FromTo("fish_cod_raw", vTo: Version.V112X).FromTo("cod", Version.V113X);
        items.AddTexture(5, 10).FromTo("fish_cod_cooked", vTo: Version.V112X).FromTo("cooked_cod", Version.V113X);
        items.AddTexture(5, 12).FromTo("cookie");
        items.AddTexture(5, 13).FromTo("shears");
        items.AddTexture(5, 14).FromTo("dye_powder_red", vTo: Version.V112X)
            .FromTo("rose_red", Version.V113X, Version.V113X).FromTo("red_dye", Version.V114X);
        items.AddTexture(5, 15).FromTo("dye_powder_pink", vTo: Version.V112X).FromTo("pink_dye", Version.V113X);
        items.AddTexture(6, 0).FromTo("wood_pickaxe", vTo: Version.V112X).FromTo("wooden_pickaxe", Version.V113X);
        items.AddTexture(6, 1).FromTo("stone_pickaxe");
        items.AddTexture(6, 2).FromTo("iron_pickaxe");
        items.AddTexture(6, 3).FromTo("gold_pickaxe", vTo: Version.V112X).FromTo("golden_pickaxe", Version.V113X);
        items.AddTexture(6, 4).FromTo("diamond_pickaxe");
        items.AddTexture(6, 7).FromTo("leather");
        items.AddTexture(6, 8).FromTo("saddle");
        items.AddTexture(6, 14).FromTo("dye_powder_green", vTo: Version.V112X)
            .FromTo("cactus_green", Version.V113X, Version.V113X).FromTo("green_dye", Version.V114X);
        items.AddTexture(6, 15).FromTo("dye_powder_lime", vTo: Version.V112X).FromTo("lime_dye", Version.V113X);
        items.AddTexture(7, 0).FromTo("wood_axe", vTo: Version.V112X).FromTo("wooden_axe", Version.V113X);
        items.AddTexture(7, 1).FromTo("stone_axe");
        items.AddTexture(7, 2).FromTo("iron_axe");
        items.AddTexture(7, 3).FromTo("gold_axe", vTo: Version.V112X).FromTo("golden_axe", Version.V113X);
        items.AddTexture(7, 4).FromTo("diamond_axe");
        items.AddTexture(7, 14).FromTo("dye_powder_brown", vTo: Version.V112X).FromTo("cocoa_beans", Version.V113X);
        items.AddTexture(7, 15).FromTo("dye_powder_yellow", vTo: Version.V112X)
            .FromTo("dandelion_yellow", Version.V113X, Version.V113X).FromTo("yellow_dye", Version.V114X);
        items.AddTexture(8, 0).FromTo("wood_hoe", vTo: Version.V112X).FromTo("wooden_hoe", Version.V113X);
        items.AddTexture(8, 1).FromTo("stone_hoe");
        items.AddTexture(8, 2).FromTo("iron_hoe");
        items.AddTexture(8, 3).FromTo("gold_hoe", vTo: Version.V112X).FromTo("golden_hoe", Version.V113X);
        items.AddTexture(8, 4).FromTo("diamond_hoe");
        items.AddTexture(8, 7).FromTo("minecart_normal", vTo: Version.V112X).FromTo("minecart", Version.V113X);
        items.AddTexture(8, 8).FromTo("boat", vTo: Version.V18X).FromTo("oak_boat", Version.V19X);
        items.AddTexture(8, 14).FromTo("dye_powder_blue", vTo: Version.V112X).FromTo("lapis_lazuli", Version.V113X);
        items.AddTexture(8, 15).FromTo("dye_powder_light_blue", vTo: Version.V112X)
            .FromTo("light_blue_dye", Version.V113X);
        items.AddTexture(9, 7).FromTo("minecart_chest", vTo: Version.V112X).FromTo("chest_minecart", Version.V113X);
        items.AddTexture(9, 14).FromTo("dye_powder_purple", vTo: Version.V112X).FromTo("purple_dye", Version.V113X);
        items.AddTexture(9, 15).FromTo("dye_powder_magenta", vTo: Version.V112X).FromTo("magenta_dye", Version.V113X);
        items.AddTexture(10, 7).FromTo("minecart_furnace", vTo: Version.V112X)
            .FromTo("furnace_minecart", Version.V113X);
        items.AddTexture(10, 14).FromTo("dye_powder_cyan", vTo: Version.V112X).FromTo("cyan_dye", Version.V113X);
        items.AddTexture(10, 15).FromTo("dye_powder_orange", vTo: Version.V112X).FromTo("orange_dye", Version.V113X);
        items.AddTexture(11, 14).FromTo("dye_powder_cyan", vTo: Version.V112X).FromTo("cyan_dye", Version.V113X);
        items.AddTexture(11, 15).FromTo("dye_powder_orange", vTo: Version.V112X).FromTo("orange_dye", Version.V113X);
        items.AddTexture(11, 14).FromTo("dye_powder_silver", vTo: Version.V112X)
            .FromTo("light_gray_dye", Version.V113X);
        items.AddTexture(11, 15).FromTo("dye_powder_white", vTo: Version.V112X).FromTo("bone_meal", Version.V113X);
        items.AddTexture(15, 0).FromTo("record_13", vTo: Version.V112X).FromTo("music_disc_13", Version.V113X);
        items.AddTexture(15, 1).FromTo("record_cat", vTo: Version.V112X).FromTo("music_disc_cat", Version.V113X);

        #endregion

        #region ENTITY

        entity = new TextureBundle(resolution);
        entity.AddTexture("char").FromTo("steve", vTo: Version.V118X).FromTo(@"player\wide\steve", Version.V119X);
        entity.AddTexture("chicken").FromTo("chicken");
        entity.AddTexture("cow").FromTo(@"cow\cow");
        entity.AddTexture("creeper").FromTo(@"creeper\creeper");
        entity.AddTexture("ghast").FromTo(@"ghast\ghast");
        entity.AddTexture("ghast_fire").FromTo(@"ghast\ghast_shooting");
        entity.AddTexture("pig").FromTo(@"pig\pig");
        entity.AddTexture("pigzombie").FromTo(@"zombie_pigman", vTo: Version.V118X).FromTo(@"piglin\piglin", Version.V119X);
        entity.AddTexture("saddle").FromTo(@"pig\pig_saddle");
        entity.AddTexture("sheep").FromTo(@"sheep\sheep");
        entity.AddTexture("sheep_fur").FromTo(@"sheep\sheep_fur");
        entity.AddTexture("skeleton").FromTo(@"skeleton\skeleton");
        entity.AddTexture("slime").FromTo(@"slime\slime");
        entity.AddTexture("spider").FromTo(@"spider\spider");
        entity.AddTexture("spider_eyes").FromTo(@"spider_eyes");
        entity.AddTexture("squid").FromTo("squid", vTo: Version.V116X).FromTo(@"squid\squid", Version.V119X);
        entity.AddTexture("wolf").FromTo(@"wolf\wolf");
        entity.AddTexture("wolf_angry").FromTo(@"wolf\wolf_angry");
        entity.AddTexture("wolf_tame").FromTo(@"wolf\wolf_tame");
        entity.AddTexture("zombie").FromTo(@"zombie\zombie");


        #endregion

        #region ARMOR

        armor = new TextureBundle(resolution);
        armor.AddTexture("chain_1").FromTo("chainmail_layer_1");
        armor.AddTexture("chain_2").FromTo("chainmail_layer_2");
        armor.AddTexture("diamond_1").FromTo("diamond_layer_1");
        armor.AddTexture("diamond_2").FromTo("diamond_layer_2");
        armor.AddTexture("gold_1").FromTo("gold_layer_1");
        armor.AddTexture("gold_2").FromTo("gold_layer_2");
        armor.AddTexture("iron_1").FromTo("iron_layer_1");
        armor.AddTexture("iron_2").FromTo("iron_layer_2");

        #endregion

        //terrain.DrawAtlas(version, blocksFolder, @"backport\terrain.png", terrainTemplateAtlas);
        //items.DrawAtlas(version, itemsFolder, @"backport\gui\items.png", itemTemplateAtlas);
        if (string.IsNullOrEmpty(terrainTemplateAtlas))
            await Task.Run(() => terrain.DrawAtlas(version, blocksFolder, @"backport\terrain.png", TerrainTemplate));
        else
            await Task.Run(() => terrain.DrawAtlas(version, blocksFolder, @"backport\terrain.png", terrainTemplateAtlas));

        if (string.IsNullOrEmpty(itemTemplateAtlas))
            await Task.Run(() => items.DrawAtlas(version, itemsFolder, @"backport\gui\items.png", ItemTemplate));
        else
            await Task.Run(() => items.DrawAtlas(version, itemsFolder, @"backport\gui\items.png", itemTemplateAtlas));

        entity.DrawBundle(version, entityFolder, @"backport\mob");
        armor.DrawBundle(version, armorFolder, @"backport\armor");

        if (File.Exists(@"texture_source\pack.png"))
            File.Copy(@"texture_source\pack.png", @"backport\pack.png");

        if (File.Exists(Path.Combine(textureFolder, @"gui\options_background.png")))
            File.Copy(Path.Combine(textureFolder, @"gui\options_background.png"), @"backport\gui\background.png");

        if (File.Exists(Path.Combine(textureFolder, @"gui\widgets.png")))
            File.Copy(Path.Combine(textureFolder, @"gui\widgets.png"), @"backport\gui\gui.png");

        if (File.Exists(Path.Combine(textureFolder, @"gui\icons.png")))
            File.Copy(Path.Combine(textureFolder, @"gui\icons.png"), @"backport\gui\icons.png");
        if (File.Exists(Path.Combine(blocksFolder, @"gui\icons.png")))
            File.Copy(Path.Combine(blocksFolder, @"gui\icons.png"), @"backport\gui\icons.png");

        var fileNamePrefix = "backport_";
        var targetFile = Path.Combine(targetDirectory, fileNamePrefix + Path.GetFileName(sourceFile));
        while (File.Exists(targetFile))
        {
            fileNamePrefix += '_';
            targetFile = Path.Combine(targetDirectory, fileNamePrefix + Path.GetFileName(sourceFile));
        }
            

        ZipFile.CreateFromDirectory("backport", targetFile);
        
        Directory.Delete("texture_source", true);
        Directory.Delete("backport", true);

        OnPortFinished?.Invoke();
    }
}