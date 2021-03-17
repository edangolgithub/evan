<?php
/**
 * Gallery options.
 *
 * @package Creativ Kids Education
 */

$default = creativ_kids_education_get_default_theme_options();

// Featured Gallery Section
$wp_customize->add_section( 'section_home_gallery',
	array(
		'title'      => __( 'Gallery Section', 'creativ-kids-education' ),
		'priority'   => 100,
		'capability' => 'edit_theme_options',
		'panel'      => 'home_page_panel',
		)
);
// Disable Gallery Section
$wp_customize->add_setting('theme_options[disable_gallery_section]', 
	array(
	'default' 			=> $default['disable_gallery_section'],
	'type'              => 'theme_mod',
	'capability'        => 'edit_theme_options',
	'sanitize_callback' => 'creativ_kids_education_sanitize_checkbox'
	)
);

$wp_customize->add_control('theme_options[disable_gallery_section]', 
	array(		
	'label' 	=> __('Disable Gallery Section', 'creativ-kids-education'),
	'section' 	=> 'section_home_gallery',
	'settings'  => 'theme_options[disable_gallery_section]',
	'type' 		=> 'checkbox',	
	)
);

//Gallery Section title
$wp_customize->add_setting('theme_options[gallery_title]', 
	array(
	'default'           => $default['gallery_title'],
	'type'              => 'theme_mod',
	'capability'        => 'edit_theme_options',	
	'sanitize_callback' => 'sanitize_text_field'
	)
);

$wp_customize->add_control('theme_options[gallery_title]', 
	array(
	'label'       => __('Section Title', 'creativ-kids-education'),
	'section'     => 'section_home_gallery',   
	'settings'    => 'theme_options[gallery_title]',
	'active_callback' => 'creativ_kids_education_gallery_active',		
	'type'        => 'text'
	)
);

// Number of Gallery
$wp_customize->add_setting('theme_options[number_of_gy_column]', 
	array(
	'default' 			=> $default['number_of_gy_column'],
	'type'              => 'theme_mod',
	'capability'        => 'edit_theme_options',	
	'sanitize_callback' => 'creativ_kids_education_sanitize_number_range'
	)
);

$wp_customize->add_control('theme_options[number_of_gy_column]', 
	array(
	'label'       => __('Column Per Row', 'creativ-kids-education'),
	'description' => __('Save & Refresh the customizer to see its effect. Maximum is 4', 'creativ-kids-education'),
	'section'     => 'section_home_gallery',   
	'settings'    => 'theme_options[number_of_gy_column]',		
	'type'        => 'number',
	'active_callback' => 'creativ_kids_education_gallery_active',
	'input_attrs' => array(
			'min'	=> 1,
			'max'	=> 4,
			'step'	=> 1,
		),
	)
);
// Number of items
$wp_customize->add_setting('theme_options[number_of_gy_items]', 
	array(
	'default' 			=> $default['number_of_gy_items'],
	'type'              => 'theme_mod',
	'capability'        => 'edit_theme_options',	
	'sanitize_callback' => 'creativ_kids_education_sanitize_number_range'
	)
);

$wp_customize->add_control('theme_options[number_of_gy_items]', 
	array(
	'label'       => __('Number Of Gallery', 'creativ-kids-education'),
	'description' => __('Save & Refresh the customizer to see its effect. Maximum is 8.', 'creativ-kids-education'),
	'section'     => 'section_home_gallery',   
	'settings'    => 'theme_options[number_of_gy_items]',		
	'type'        => 'number',
	'active_callback' => 'creativ_kids_education_gallery_active',
	'input_attrs' => array(
			'min'	=> 1,
			'max'	=> 8,
			'step'	=> 1,
		),
	)
);

$wp_customize->add_setting('theme_options[gy_content_type]', 
	array(
	'default' 			=> $default['gy_content_type'],
	'type'              => 'theme_mod',
	'capability'        => 'edit_theme_options',	
	'sanitize_callback' => 'creativ_kids_education_sanitize_select'
	)
);

$wp_customize->add_control('theme_options[gy_content_type]', 
	array(
	'label'       => __('Gallery Type', 'creativ-kids-education'),
	'section'     => 'section_home_gallery',   
	'settings'    => 'theme_options[gy_content_type]',		
	'type'        => 'select',
	'active_callback' => 'creativ_kids_education_gallery_active',
	'choices'	  => array(
			'gy_page'	  => __('Page','creativ-kids-education'),
		),
	)
);

$number_of_gy_items = creativ_kids_education_get_option( 'number_of_gy_items' );

for( $i=1; $i<=$number_of_gy_items; $i++ ){

	// Additional Information First Page
	$wp_customize->add_setting('theme_options[gallery_page_'.$i.']', 
		array(
		'type'              => 'theme_mod',
		'capability'        => 'edit_theme_options',	
		'sanitize_callback' => 'creativ_kids_education_dropdown_pages'
		)
	);

	$wp_customize->add_control('theme_options[gallery_page_'.$i.']', 
		array(
		'label'       => sprintf( __('Select Page #%1$s', 'creativ-kids-education'), $i),
		'section'     => 'section_home_gallery',   
		'settings'    => 'theme_options[gallery_page_'.$i.']',		
		'type'        => 'dropdown-pages',
		'active_callback' => 'creativ_kids_education_gallery_page',
		)
	);
}