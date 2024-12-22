shader_type canvas_item;
uniform sampler2D screen_texture : hint_screen_texture, filter_linear_mipmap;
uniform float value = 1.15;
void fragment(){
	COLOR = textureLod(screen_texture, SCREEN_UV, value);
}