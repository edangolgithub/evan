<?php
/**
 * Additional Information options.
 *
 * @package Creativ Kids Education
 */

$default = creativ_kids_education_get_default_theme_options();

// Featured Additional Information Section
$wp_customize->add_section( 'section_additional_info',
	array(
		'title'      => __( 'Our Features', 'creativ-kids-education' ),
		'priority'   => 100,
		'capability' => 'edit_theme_options',
		'panel'      => 'home_page_panel',
		)
);
// Disable Additional Information Section
$wp_customize->add_setting('theme_options[disable_additional_info_section]', 
	array(
	'default' 			=> $default['disable_additional_info_section'],
	'type'              => 'theme_mod',
	'capability'        => 'edit_theme_options',
	'sanitize_callback' => 'creativ_kids_education_sanitize_checkbox'
	)
);

$wp_customize->add_control('theme_options[disable_additional_info_section]', 
	array(		
	'label' 	=> __('Disable Additional Information Section', 'creativ-kids-education'),
	'section' 	=> 'section_additional_info',
	'settings'  => 'theme_options[disable_additional_info_section]',
	'type' 		=> 'checkbox',	
	)
);

//Additional Information Section title
$wp_customize->add_setting('theme_options[additional_info_section_title]', 
	array(
	'default'           => $default['additional_info_section_title'],
	'type'              => 'theme_mod',
	'capability'        => 'edit_theme_options',	
	'sanitize_callback' => 'sanitize_text_field'
	)
);

$wp_customize->add_control('theme_options[additional_info_section_title]', 
	array(
	'label'       => __('Section Title', 'creativ-kids-education'),
	'section'     => 'section_additional_info',   
	'settings'    => 'theme_options[additional_info_section_title]',		
	'type'        => 'text',
	'active_callback' => 'creativ_kids_education_additional_info_active',
	)
);

// Number of counter
$wp_customize->add_setting('theme_options[number_of_column]', 
	array(
	'default' 			=> $default['number_of_column'],
	'type'              => 'theme_mod',
	'capability'        => 'edit_theme_options',	
	'sanitize_callback' => 'creativ_kids_education_sanitize_number_range'
	)
);

$wp_customize->add_control('theme_options[number_of_column]', 
	array(
	'label'       => __('Column Per Row', 'creativ-kids-education'),
	'description' => __('Save & Refresh the customizer to see its effect. Maximum is 3', 'creativ-kids-education'),
	'section'     => 'section_additional_info',   
	'settings'    => 'theme_options[number_of_column]',		
	'type'        => 'number',
	'active_callback' => 'creativ_kids_education_additional_info_active',
	'input_attrs' => array(
			'min'	=> 1,
			'max'	=> 3,
			'step'	=> 1,
		),
	)
);
// Number of items
$wp_customize->add_setting('theme_options[number_of_items]', 
	array(
	'default' 			=> $default['number_of_items'],
	'type'              => 'theme_mod',
	'capability'        => 'edit_theme_options',	
	'sanitize_callback' => 'creativ_kids_education_sanitize_number_range'
	)
);

$wp_customize->add_control('theme_options[number_of_items]', 
	array(
	'label'       => __('Number Of Items', 'creativ-kids-education'),
	'description' => __('Save & Refresh the customizer to see its effect. Maximum is 6.', 'creativ-kids-education'),
	'section'     => 'section_additional_info',   
	'settings'    => 'theme_options[number_of_items]',		
	'type'        => 'number',
	'active_callback' => 'creativ_kids_education_additional_info_active',
	'input_attrs' => array(
			'min'	=> 1,
			'max'	=> 6,
			'step'	=> 1,
		),
	)
);

$wp_customize->add_setting('theme_options[ad_content_type]', 
	array(
	'default' 			=> $default['ad_content_type'],
	'type'              => 'theme_mod',
	'capability'        => 'edit_theme_options',	
	'sanitize_callback' => 'creativ_kids_education_sanitize_select'
	)
);

$wp_customize->add_control('theme_options[ad_content_type]', 
	array(
	'label'       => __('Content Type', 'creativ-kids-education'),
	'section'     => 'section_additional_info',   
	'settings'    => 'theme_options[ad_content_type]',		
	'type'        => 'select',
	'active_callback' => 'creativ_kids_education_additional_info_active',
	'choices'	  => array(
			'ad_page'	  => __('Page','creativ-kids-education'),
		),
	)
);

$number_of_items = creativ_kids_education_get_option( 'number_of_items' );

for( $i=1; $i<=$number_of_items; $i++ ){

	// Additional Information First Icon
	$wp_customize->add_setting('theme_options[additional_info_icon_'.$i.']', 
		array(
		'type'              => 'theme_mod',
		'capability'        => 'edit_theme_options',	
		'sanitize_callback' => 'sanitize_text_field'
		)
	);

	$wp_customize->add_control('theme_options[additional_info_icon_'.$i.']', 
		array(
		'label'       => sprintf( __('Select Icon #%1$s', 'creativ-kids-education'), $i),
		'description' => sprintf( __('Please input icon as eg: fa fa-archive. Find Font-awesome icons %1$shere%2$s', 'creativ-kids-education'), '<a href="' . esc_url( 'https://fontawesome.com/cheatsheet' ) . '" target="_blank">', '</a>' ),
		'section'     => 'section_additional_info',   
		'settings'    => 'theme_options[additional_info_icon_'.$i.']',		
		'active_callback' => 'creativ_kids_education_additional_info_active',			
		'type'        => 'text',
		)
	);

	// Additional Information First Page
	$wp_customize->add_setting('theme_options[additional_info_'.$i.']', 
		array(
		'type'              => 'theme_mod',
		'capability'        => 'edit_theme_options',	
		'sanitize_callback' => 'creativ_kids_education_dropdown_pages'
		)
	);

	$wp_customize->add_control('theme_options[additional_info_'.$i.']', 
		array(
		'label'       => sprintf( __('Select Page #%1$s', 'creativ-kids-education'), $i),
		'section'     => 'section_additional_info',   
		'settings'    => 'theme_options[additional_info_'.$i.']',		
		'type'        => 'dropdown-pages',
		'active_callback' => 'creativ_kids_education_additional_info_page',
		)
	);
	
}