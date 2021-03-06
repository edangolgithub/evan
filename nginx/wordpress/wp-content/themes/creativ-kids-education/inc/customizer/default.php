<?php
/**
 * Default theme options.
 *
 * @package Creativ Kids Education
 */

if ( ! function_exists( 'creativ_kids_education_get_default_theme_options' ) ) :

	/**
	 * Get default theme options.
	 *
	 * @since 1.0.0
	 *
	 * @return array Default theme options.
	 */
function creativ_kids_education_get_default_theme_options() {

	$defaults = array();

	$defaults['show_header_contact_info'] 	= true;
    $defaults['header_email']             	= __( 'example@gmail.com','creativ-kids-education' );
    $defaults['header_phone' ]            	= __( '+1-541-754-3010','creativ-kids-education' );
    $defaults['header_location' ]           = __( 'London, UK','creativ-kids-education' );
    $defaults['show_header_social_links'] 	= true;
    $defaults['header_social_links']		= array();

    // Homepage Options
	$defaults['enable_frontpage_content'] 	= true;

	// Featured Slider Section
	$defaults['disable_featured_slider']	= true;
	$defaults['number_of_sr_items']			= 3;
	$defaults['sr_content_type']			= 'sr_page';

	// Our Features Section
	$defaults['disable_additional_info_section']	= true;
	$defaults['additional_info_section_title']	   	= esc_html__( 'Our Features', 'creativ-kids-education' );
	$defaults['number_of_column']					= 3;
	$defaults['number_of_items']					= 6;
	$defaults['ad_content_type']					= 'ad_page';

	//Featured Courses Section	
	$defaults['disable_courses_section']	= true;
	$defaults['courses_section_title']	   	= esc_html__( 'Our Courses', 'creativ-kids-education' );
	$defaults['number_of_cs_column']		= 3;
	$defaults['number_of_cs_items']			= 3;
	$defaults['cs_content_type']			= 'cs_page';

	//Cta Section	
	$defaults['disable_cta_section']	   	= true;
	$defaults['background_cta_section']		= get_template_directory_uri() .'/assets/images/default-header.jpg';
	$defaults['cta_small_description']	   	= esc_html__( 'Need More Information?', 'creativ-kids-education' );
	$defaults['cta_description']	   	 	= esc_html__( 'Lorem ipsum dolor sit amet consectetur adipisicing elit sed do eiusmod.', 'creativ-kids-education' );
	$defaults['cta_button_label']	   	 	= esc_html__( 'Purchase Now', 'creativ-kids-education' );
	$defaults['cta_button_url']	   	 		= '#';

	// Gallery Section
	$defaults['disable_gallery_section']	= true;
	$defaults['gallery_title']	   	 		= esc_html__( 'Amazing Projects', 'creativ-kids-education' );
	$defaults['number_of_gy_column']		= 4;
	$defaults['number_of_gy_items']			= 8;
	$defaults['gy_content_type']			= 'gy_page';

	// Blog Section
	$defaults['disable_blog_section']		= true;
	$defaults['latest_blog_title']	   	 	= esc_html__( 'Latest News', 'creativ-kids-education' );
	$defaults['blog_category']	   			= 0; 
	$defaults['blog_number']				= 3;

	//General Section
	$defaults['readmore_text']				= esc_html__('Read More','creativ-kids-education');
	$defaults['your_latest_posts_title']	= esc_html__('Blog','creativ-kids-education');
	$defaults['excerpt_length']				= 40;
	$defaults['layout_options']				= 'right-sidebar';	

	//Footer section 		
	$defaults['copyright_text']				= esc_html__( 'Copyright &copy; All rights reserved.', 'creativ-kids-education' );

	// Pass through filter.
	$defaults = apply_filters( 'creativ_kids_education_filter_default_theme_options', $defaults );
	return $defaults;
}

endif;

/**
*  Get theme options
*/
if ( ! function_exists( 'creativ_kids_education_get_option' ) ) :

	/**
	 * Get theme option
	 *
	 * @since 1.0.0
	 *
	 * @param string $key Option key.
	 * @return mixed Option value.
	 */
	function creativ_kids_education_get_option( $key ) {

		$default_options = creativ_kids_education_get_default_theme_options();
		if ( empty( $key ) ) {
			return;
		}

		$theme_options = (array)get_theme_mod( 'theme_options' );
		$theme_options = wp_parse_args( $theme_options, $default_options );

		$value = null;

		if ( isset( $theme_options[ $key ] ) ) {
			$value = $theme_options[ $key ];
		}

		return $value;

	}

endif;