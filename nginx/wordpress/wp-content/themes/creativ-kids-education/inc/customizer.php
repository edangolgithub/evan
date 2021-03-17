<?php
/**
 * Creativ Kids Education Theme Customizer
 *
 * @package Creativ Kids Education
 */

/**
 * Add postMessage support for site title and description for the Theme Customizer.
 *
 * @param WP_Customize_Manager $wp_customize Theme Customizer object.
 */
function creativ_kids_education_customize_register( $wp_customize ) {
	$wp_customize->get_setting( 'blogname' )->transport         = 'postMessage';
	$wp_customize->get_setting( 'blogdescription' )->transport  = 'postMessage';
	$wp_customize->get_setting( 'header_textcolor' )->transport = 'postMessage';

	// Register custom section types.
	$wp_customize->register_section_type( 'creativ_kids_education_Customize_Section_Upsell' );

	// Register sections.
	$wp_customize->add_section(
		new creativ_kids_education_Customize_Section_Upsell(
			$wp_customize,
			'theme_upsell',
			array(
				'title'    => esc_html__( 'Creativ Kids Education Pro', 'creativ-kids-education' ),
				'pro_text' => esc_html__( 'Buy Pro', 'creativ-kids-education' ),
				'pro_url'  => 'http://www.creativthemes.com/downloads/creativ-kids-education-pro/',
				'priority'  => 10,
			)
		)
	);

	// Load customize sanitize.
	include get_template_directory() . '/inc/customizer/sanitize.php';

	// Load customize sanitize.
	include get_template_directory() . '/inc/customizer/active-callback.php';

	// Load topbar sections option.
	include get_template_directory() . '/inc/customizer/topbar.php';

	// Load header sections option.
	include get_template_directory() . '/inc/customizer/theme-section.php';

	// Load home page sections option.
	include get_template_directory() . '/inc/customizer/home-section.php';
	
}
add_action( 'customize_register', 'creativ_kids_education_customize_register' );

/**
 * Binds JS handlers to make Theme Customizer preview reload changes asynchronously.
 */
function creativ_kids_education_customize_preview_js() {
	wp_enqueue_script( 'creativ_kids_education_customizer', get_template_directory_uri() . '/inc/customizer/js/customizer.js', array( 'customize-preview' ), '20151215', true );
}
add_action( 'customize_preview_init', 'creativ_kids_education_customize_preview_js' );
/**
 *
 */
function creativ_kids_education_customize_backend_scripts() {

	wp_enqueue_style( 'Creativ Kids Education-admin-customizer-style', get_template_directory_uri() . '/inc/customizer/css/customizer-style.css' );
	wp_enqueue_script( 'Creativ Kids Education-admin-customizer', get_template_directory_uri() . '/inc/customizer/js/customizer-scipt.js', array( 'jquery', 'customize-controls' ), '20151215', true );
}
add_action( 'customize_controls_enqueue_scripts', 'creativ_kids_education_customize_backend_scripts', 10 );
