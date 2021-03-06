<?php 
/**
 * Template part for displaying About Section
 *
 *@package Creativ Kids Education
 */
?>
    <?php 
        $cta_small_description  = creativ_kids_education_get_option( 'cta_small_description' );
        $cta_description        = creativ_kids_education_get_option( 'cta_description' );
        $cta_button_label       = creativ_kids_education_get_option( 'cta_button_label' );
        $cta_button_url         = creativ_kids_education_get_option( 'cta_button_url' );
    ?>

    <?php if ( !empty($cta_description ) )  :?>
        <div class="section-header">
            <h2 class="section-title"><?php echo esc_html($cta_small_description); ?></h2>
            <p><?php echo esc_html($cta_description); ?></p>
        </div><!-- .section-header -->
    <?php endif;?>

    <?php if ( !empty($cta_button_label ) )  :?>
        <div class="read-more">
            <a href="<?php echo esc_url($cta_button_url); ?>" class="btn"><?php echo esc_html($cta_button_label); ?></a>
        </div><!-- .read-more -->
    <?php endif;?>
