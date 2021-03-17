<?php 
/**
 * Template part for displaying Gallery Section
 *
 *@package Creativ Kids Education
 */
    $gallery_title       = creativ_kids_education_get_option( 'gallery_title' );
    $gy_content_type     = creativ_kids_education_get_option( 'gy_content_type' );
    $number_of_gy_column = creativ_kids_education_get_option( 'number_of_gy_column' );
    $number_of_gy_items  = creativ_kids_education_get_option( 'number_of_gy_items' );
    for( $i=1; $i<=$number_of_gy_items; $i++ ) :
        $gallery_page_posts[] = absint(creativ_kids_education_get_option( 'gallery_page_'.$i ) );
    endfor;
    ?>

    <?php if(!empty($gallery_title)):?>
        <div class="section-header">
            <h2 class="section-title"><span class="shape shape-left"></span><span class="title"><?php echo esc_html($gallery_title);?></span><span class="shape shape-right"></span></h2>
        </div><!-- .section-header -->
    <?php endif;?>

    <?php if( $gy_content_type == 'gy_page' ) : ?>
        <div class="section-content col-<?php echo esc_attr( $number_of_gy_column ); ?>">
            <?php $args = array (
                'post_type'     => 'page',
                'post_per_page' => count( $gallery_page_posts ),
                'post__in'      => $gallery_page_posts,
                'orderby'       =>'post__in',
            );        
            $loop = new WP_Query($args);                        
            if ( $loop->have_posts() ) :
            $i=-1;  
                while ($loop->have_posts()) : $loop->the_post(); $i++;?>
                
                <article>
                    <div class="overlay"></div>
                    <?php if ( has_post_thumbnail() ) : ?>
                        <div class="featured-image">
                            <a href="<?php the_permalink(); ?>" title="<?php the_title_attribute(); ?>">
                                <img src="<?php the_post_thumbnail_url(); ?>"/>
                            </a>
                        </div><!-- .featured-image -->
                    <?php endif; ?>

                    <div class="entry-container">
                        <header class="entry-header">
                            <h2 class="entry-title"><a href="<?php the_permalink();?>"><?php the_title();?></a></h2>
                        </header>
                    </div><!-- .entry-container -->
                </article>

              <?php endwhile;?>
              <?php wp_reset_postdata(); ?>
            <?php endif;?>
        </div>
    <?php endif;