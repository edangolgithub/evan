<?php
/**
 * Theme functions related to structure.
 *
 * This file contains structural hook functions.
 *
 * @package Creativ Kids Education
 */

if ( ! function_exists( 'creativ_kids_education_doctype' ) ) :
	/**
	 * Doctype Declaration.
	 *
	 * @since 1.0.0
	 */
function creativ_kids_education_doctype() {
	?><!DOCTYPE html> <html <?php language_attributes(); ?>><?php
}
endif;

add_action( 'creativ_kids_education_action_doctype', 'creativ_kids_education_doctype', 10 );


if ( ! function_exists( 'creativ_kids_education_head' ) ) :
	/**
	 * Header Codes.
	 *
	 * @since 1.0.0
	 */
function creativ_kids_education_head() {
	?>
	<meta charset="<?php bloginfo( 'charset' ); ?>">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="profile" href="http://gmpg.org/xfn/11">
	<link rel="pingback" href="<?php bloginfo( 'pingback_url' ); ?>">
	<?php
}
endif;
add_action( 'creativ_kids_education_action_head', 'creativ_kids_education_head', 10 );

if ( ! function_exists( 'creativ_kids_education_page_start' ) ) :
	/**
	 * Add Skip to content.
	 *
	 * @since 1.0.0
	 */
	function creativ_kids_education_page_start() {
	?><div id="page" class="site"><a class="skip-link screen-reader-text" href="#content"><?php esc_html_e( 'Skip to content', 'creativ-kids-education' ); ?></a><?php
	}
endif;

add_action( 'creativ_kids_education_action_before', 'creativ_kids_education_page_start', 10 );

if ( ! function_exists( 'creativ_kids_education_header_start' ) ) :
	/**
	 * Header Start.
	 *
	 * @since 1.0.0
	 */
	function creativ_kids_education_header_start() {

        $show_contact = creativ_kids_education_get_option( 'show_header_contact_info' );
        $location     = creativ_kids_education_get_option( 'header_location' );
        $email        = creativ_kids_education_get_option( 'header_email' );
        $phone        = creativ_kids_education_get_option( 'header_phone' ); 
        $show_social  = creativ_kids_education_get_option( 'show_header_social_links' );
        $class = 'col-1';

        if( ( ( ! empty( $email ) || ! empty( $phone ) || ! empty( $location ) ) && $show_contact ) && ( $show_social ) ) {
            $class = 'col-2';
        }

        if( $show_contact || $show_social ){ ?>
    
            <div id="top-bar" class="top-bar-widgets <?php echo esc_attr( $class ); ?>">
                <div class="wrapper">
                    <?php if( ( ! empty( $email ) || ! empty( $phone ) || ! empty( $location ) ) && $show_contact ) : ?>
                        
                        <div class="widget widget_address_block">
                            <ul>
                                <?php 

                                    if( ! empty( $location ) ){
                                        echo '<li><i class="fa fa-map-marker"></i>'. esc_html( $location ) .'</li>';
                                    }
                                    if( ! empty( $phone ) ){
                                        echo '<li><i class="fa fa-phone"></i><a href="tel:'. esc_attr( preg_replace( '/\D/', '', $phone ) ) .'" class="phone">'. esc_html( $phone ) .'</a></li>';
                                    }
                                    if( ! empty( $email ) ){
                                        echo '<li><i class="fa fa-envelope"></i><a href="mailto:'. sanitize_email( $email ) .'" class="email">'. esc_html( $email ) .'</a></li>';
                                    }
                                ?>
                            </ul>
                        </div><!-- .widget_address_block -->
                    <?php endif; 

                    if ( $show_social ){ ?>
                        <div class="widget widget_social_icons">
                           <?php creativ_kids_education_render_social_links(); ?>
                        </div><!-- .widget_social_icons -->
                    <?php } ?>
                </div><!-- .wrapper -->
            </div><!-- #top-bar -->
        <?php
        } ?>
		<header id="masthead" class="site-header" role="banner"><?php
	}
endif;
add_action( 'creativ_kids_education_action_before_header', 'creativ_kids_education_header_start' );

if ( ! function_exists( 'creativ_kids_education_header_end' ) ) :
	/**
	 * Header Start.
	 *
	 * @since 1.0.0
	 */
	function creativ_kids_education_header_end() {

		?></header> <!-- header ends here --><?php
	}
endif;
add_action( 'creativ_kids_education_action_header', 'creativ_kids_education_header_end', 15 );

if ( ! function_exists( 'creativ_kids_education_content_start' ) ) :
	/**
	 * Header End.
	 *
	 * @since 1.0.0
	 */
	function creativ_kids_education_content_start() { 
	?>
	<div id="content" class="site-content">
	<?php 

	}
endif;

add_action( 'creativ_kids_education_action_before_content', 'creativ_kids_education_content_start', 10 );

if ( ! function_exists( 'creativ_kids_education_footer_start' ) ) :
	/**
	 * Footer Start.
	 *
	 * @since 1.0.0
	 */
	function creativ_kids_education_footer_start() {
		if( !(is_home() || is_front_page()) ){
			echo '</div>';
		} ?>
		</div>
		<footer id="colophon" class="site-footer" role="contentinfo"><?php
	}
endif;
add_action( 'creativ_kids_education_action_before_footer', 'creativ_kids_education_footer_start' );

if ( ! function_exists( 'creativ_kids_education_footer_end' ) ) :
	/**
	 * Footer End.
	 *
	 * @since 1.0.0
	 */
	function creativ_kids_education_footer_end() {?>
		</footer><?php
	}
endif;
add_action( 'creativ_kids_education_action_after_footer', 'creativ_kids_education_footer_end' );
