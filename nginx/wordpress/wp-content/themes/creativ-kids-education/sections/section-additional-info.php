<?php 
/**
 * Template part for displaying additional_info Section
 *
 *@package Creativ Kids Education
 */

$ad_content_type   = creativ_kids_education_get_option( 'ad_content_type' );
$number_of_column  = creativ_kids_education_get_option( 'number_of_column' );
$number_of_items = creativ_kids_education_get_option( 'number_of_items' );
$additional_info_section_title      = creativ_kids_education_get_option( 'additional_info_section_title' );


for( $i=1; $i<=$number_of_items; $i++ ) :
    $additional_info_posts[] = absint(creativ_kids_education_get_option( 'additional_info_'.$i ) );
endfor;
?>

    <?php if(!empty($additional_info_section_title)):?>
        <div class="section-header">
            <h2 class="section-title"><span class="shape shape-left"></span><span class="title"><?php echo esc_html($additional_info_section_title);?></span><span class="shape shape-right"></span></h2>
        </div><!-- .section-header -->
    <?php endif;?>
    <?php if( $ad_content_type == 'ad_page' ) : ?>
        <div class="section-content clear col-<?php echo esc_attr( $number_of_column ); ?>">
            <?php $args = array (
                'post_type' => 'page',
                'post_per_page'  => count( $additional_info_posts ),
                'post__in'       => $additional_info_posts,
                'orderby'        =>'post__in',
            );
            $loop = new WP_Query($args);                        
            if ( $loop->have_posts() ) :
            $i=-1; $j=0;  
                while ($loop->have_posts()) : $loop->the_post(); $i++; $j++;
                $additional_info_icons[$j] = creativ_kids_education_get_option( 'additional_info_icon_'.$j ); ?>        
                
                <article>
                    <?php if( !empty( $additional_info_icons[$j] ) ) : ?>
                        <div class="icon-container">
                            <i class="<?php echo esc_attr( $additional_info_icons[$j] )?>"></i>
                        </div><!-- .icon-container -->
                    <?php endif; ?>
                    
                    <header class="entry-header">
                        <h2 class="entry-title"><a href="<?php the_permalink();?>"><?php the_title();?></a></h2>
                    </header>

                    <div class="entry-content">
                        <?php
                            $excerpt = creativ_kids_education_the_excerpt( 20 );
                            echo wp_kses_post( wpautop( $excerpt ) );
                        ?>
                    </div><!-- .entry-content -->
                </article>

              <?php endwhile;?>
              <?php wp_reset_postdata(); ?>
            <?php endif;?>
        </div>
    <?php endif;