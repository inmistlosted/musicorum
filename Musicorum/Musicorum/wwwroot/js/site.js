$(document).ready(function () {
    $(document).on('click', '.load-more-news', function () {
        const button = $(this);
        const container = button.closest('.all-news-container');
        const url = button.data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: data => {
                button.remove();
                container.append(data);
            },
            error: result => {
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.song-like i, .song-like-btn', function () {
        const parent = $(this).parent();
        const likesCount = +parent.find('p').text();
        const url = parent.data('url');
        const songId = parent.data('id');

        $.ajax({
            url: url,
            type: 'POST',
            data: {
                songId: songId
            },
            success: (added) => {
                if (!added  && $(this).hasClass('added')) {
                    $(this).removeClass('added');
                    $(this).parent().find('p').text(likesCount - 1);
                } else {
                    $(this).addClass('added');
                    $(this).parent().find('p').text(likesCount + 1);
                }
            },
            error: result => {
                $.fancybox.open('Щоб лайкнути пісню, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.song-page-menu .menu-item', function () {
        if (!$(this).hasClass('active')) {
            const activetab = this.classList[1];

            const menuTabs = $('.song-page-menu').find('.menu-item');
            const contentTabs = $('.song-page-content').find('.content-item');

            menuTabs.each(function (index, element) {
                if ($(element).hasClass('active')) {
                    $(element).removeClass('active');
                }

                if ($(element).hasClass(activetab)) {
                    $(element).addClass('active');
                }
            });

            contentTabs.each(function (index, element) {
                if ($(element).hasClass('active')) {
                    $(element).removeClass('active');
                }

                if ($(element).hasClass(activetab)) {
                    $(element).addClass('active');
                }
            });
        }
    });

    $(document).on('mouseover', '.song-page-comment i, .song-page-like i', function () {
        $(this).siblings('.tooltipik').css('display', 'block');
    });

    $(document).on('mouseout', '.song-page-comment i, .song-page-like i', function () {
        $(this).siblings('.tooltipik').css('display', 'none');
    });

    $(document).on('click', '.song-comment-btn', function () {
        const url = $(this).parent().data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $.fancybox.open(data);

                $('.comment-close').on('click', () => {
                    $.fancybox.close();
                });

                $('#addComment').on('submit', (event) => {
                    const form = $('#addComment');

                    if (form.find('textarea').val().length < 3) {
                        event.preventDefault();
                        form.find('span').css('display', 'block');
                    }
                });
            },
            error: result => {
                $.fancybox.open('Щоб додати коментар, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.search-btn', function () {
        const url = $(this).data('url');
        const query = $('.search-input').val();

        if (query.length) {
            window.location = `${url}?query=${query}`

            $.ajax({
                url: url,
                type: 'GET',
                data: {
                    query: query
                },
                error: result => {
                    console.error(`${result.statusText}: ${result.responseText}`);
                }
            });
        }
    });

    $(document).on('click', '.add-song-btn', function () {
        const url = $(this).data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $.fancybox.open(data);

                $('.add-song-close').on('click', () => {
                    $.fancybox.close();
                });
            },
            error: result => {
                $.fancybox.open('Щоб додати пісню, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.song-edit-btn', function () {
        const url = $(this).parent().data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $.fancybox.open(data);

                $('.add-song-close').on('click', () => {
                    $.fancybox.close();
                });
            },
            error: result => {
                $.fancybox.open('Щоб редагувати пісню, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('submit', '.edit-song-form', function () {
        //location.reload();
    });

    $(document).on('click', '.song-delete-btn', function () {
        const url = $(this).parent().data('url');
        const container = $(this).closest('.song-background');
        const songId = $(this).parent().data('id');

        $.ajax({
            url: url,
            type: 'DELETE',
            data: {
                songId: songId
            },
            success: function (message) {
                container.remove();
                $.fancybox.open(message);
            },
            error: result => {
                $.fancybox.open('Не вдалося видалити пісню');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('change', '.admin-songs-sort-select', function () {
        const url = $(this).find('option:selected').data('url');
        const container = $('.admin-content');

        $.ajax({
            url: url,
            type: 'GET',
            success: (data) => {
                container.html(data);
            },
            error: result => {
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });   
        
    });

    $(document).on('change', '.admin-songs-genre-select', function () {
        const url = $(this).find('option:selected').data('url');
        const container = $('.admin-content');

        $.ajax({
            url: url,
            type: 'GET',
            success: (data) => {
                container.html(data);
            },
            error: result => {
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });

    });

    $(document).on('click', '.admin-songs-search-btn', function () {
        let url = $(this).data('url');
        const query = $('.admin-songs-search-input').val();
        const container = $('.admin-content');

        if (query.length) {
            url = `${url}&query=${query}`

            $.ajax({
                url: url,
                type: 'GET',
                success: (data) => {
                    container.html(data);
                },
                error: result => {
                    console.error(`${result.statusText}: ${result.responseText}`);
                }
            });
        }
    });

    $(document).on('click', '.admin-songs-page', function () {
        const url = $(this).data('url');
        const container = $('.admin-content');

        $.ajax({
            url: url,
            type: 'GET',
            success: (data) => {
                container.html(data);
            },
            error: result => {
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });

    });

    $(document).on('click', '.admin-song-edit', function () {
        const url = $(this).data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $.fancybox.open(data);

                $('.add-song-close').on('click', () => {
                    $.fancybox.close();
                });
            },
            error: result => {
                $.fancybox.open('Щоб редагувати пісню, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.admin-song-delete', function () {
        const url = $(this).data('url');
        const container = $(this).closest('.admin-song');
        const songId = $(this).data('id');

        $.ajax({
            url: url,
            type: 'DELETE',
            data: {
                songId: songId
            },
            success: function (message) {
                container.remove();
                $.fancybox.open(message.message);
            },
            error: result => {
                $.fancybox.open('Не вдалося видалити пісню');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.admin-genres-search-btn', function () {
        let url = $(this).data('url');
        const query = $('.admin-genres-search-input').val();
        const container = $('.admin-content');

        if (query.length) {
            url = `${url}&query=${query}`

            $.ajax({
                url: url,
                type: 'GET',
                success: (data) => {
                    container.html(data);
                },
                error: result => {
                    console.error(`${result.statusText}: ${result.responseText}`);
                }
            });
        }
    });

    $(document).on('click', '.admin-genres-page', function () {
        const url = $(this).data('url');
        const container = $('.admin-content');

        $.ajax({
            url: url,
            type: 'GET',
            success: (data) => {
                container.html(data);
            },
            error: result => {
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });

    });

    $(document).on('click', '.admin-genre-edit', function () {
        const url = $(this).data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $.fancybox.open(data);

                $('.add-song-close').on('click', () => {
                    $.fancybox.close();
                });
            },
            error: result => {
                $.fancybox.open('Щоб редагувати жанр, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.admin-genre-delete', function () {
        const url = $(this).data('url');
        const container = $(this).closest('.admin-genre');
        const genreId = $(this).data('id');

        $.ajax({
            url: url,
            type: 'DELETE',
            data: {
                genreId: genreId
            },
            success: function (message) {
                container.remove();
                $.fancybox.open(message.message);
            },
            error: result => {
                $.fancybox.open('Не вдалося видалити жанр');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.add-genre-btn', function () {
        const url = $(this).data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $.fancybox.open(data);

                $('.add-song-close').on('click', () => {
                    $.fancybox.close();
                });
            },
            error: result => {
                $.fancybox.open('Щоб додати жанр, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('change', '.admin-news-sort-select', function () {
        const url = $(this).find('option:selected').data('url');
        const container = $('.admin-content');

        $.ajax({
            url: url,
            type: 'GET',
            success: (data) => {
                container.html(data);
            },
            error: result => {
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });

    });

    $(document).on('click', '.admin-news-search-btn', function () {
        let url = $(this).data('url');
        const query = $('.admin-news-search-input').val();
        const container = $('.admin-content');

        if (query.length) {
            url = `${url}&query=${query}`

            $.ajax({
                url: url,
                type: 'GET',
                success: (data) => {
                    container.html(data);
                },
                error: result => {
                    console.error(`${result.statusText}: ${result.responseText}`);
                }
            });
        }
    });

    $(document).on('click', '.admin-news-page', function () {
        const url = $(this).data('url');
        const container = $('.admin-content');

        $.ajax({
            url: url,
            type: 'GET',
            success: (data) => {
                container.html(data);
            },
            error: result => {
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });

    });

    $(document).on('click', '.admin-news-edit', function () {
        const url = $(this).data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $.fancybox.open(data);

                $('.add-song-close').on('click', () => {
                    $.fancybox.close();
                });
            },
            error: result => {
                $.fancybox.open('Щоб редагувати новину, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.admin-news-delete', function () {
        const url = $(this).data('url');
        const container = $(this).closest('.admin-news');
        const newsId = $(this).data('id');

        $.ajax({
            url: url,
            type: 'DELETE',
            data: {
                newsId: newsId
            },
            success: function (message) {
                container.remove();
                $.fancybox.open(message.message);
            },
            error: result => {
                $.fancybox.open('Не вдалося видалити новину');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.add-news-btn', function () {
        const url = $(this).data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $.fancybox.open(data);

                $('.add-song-close').on('click', () => {
                    $.fancybox.close();
                });
            },
            error: result => {
                $.fancybox.open('Щоб додати новину, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });
    

    $(document).on('click', '.add-event-btn', function () {
        const url = $(this).data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $.fancybox.open(data);

                $('.add-song-close').on('click', () => {
                    $.fancybox.close();
                });
            },
            error: result => {
                $.fancybox.open('Щоб додати подію, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.admin-event-edit', function () {
        const url = $(this).data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $.fancybox.open(data);

                $('.add-song-close').on('click', () => {
                    $.fancybox.close();
                });
            },
            error: result => {
                $.fancybox.open('Щоб редагувати подію, спочатку увійдіть на сайт');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.admin-event-delete', function () {
        const url = $(this).data('url');
        const container = $(this).closest('.admin-event');
        const eventId = $(this).data('id');

        $.ajax({
            url: url,
            type: 'DELETE',
            data: {
                eventId: eventId
            },
            success: function (message) {
                container.remove();
                $.fancybox.open(message.message);
            },
            error: result => {
                $.fancybox.open('Не вдалося видалити подію');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });

    $(document).on('click', '.admin-event-page', function () {
        const url = $(this).data('url');
        const container = $('.admin-content');

        $.ajax({
            url: url,
            type: 'GET',
            success: (data) => {
                container.html(data);
            },
            error: result => {
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });

    });

    $(document).on('blur', '.event-date-start, .event-date-end', function () {
        dateIsValid();
    });

    $(document).on('submit', '.add-event-form, .edit-event-form', function (e) {
        if (!dateIsValid()) {
            e.preventDefault();
        }
    });

    $(document).on('click', '.comment-del-btn', function () {
        const url = $(this).parent().data('url');
        const container = $(this).closest('.comment-item');
        const commentId = $(this).parent().data('id');

        $.ajax({
            url: url,
            type: 'DELETE',
            data: {
                commentId: commentId
            },
            success: function (message) {
                container.remove();
                $.fancybox.open(message.message);
            },
            error: result => {
                $.fancybox.open('Не вдалося видалити коментар');
                console.error(`${result.statusText}: ${result.responseText}`);
            }
        });
    });
});

function dateIsValid() {
    const dateEnd = new Date($('.event-date-end').val());
    const dateStart = new Date($('.event-date-start').val());

    if (dateEnd < dateStart) {
        $('.event-date-end-mess').text('Дата завершення події не може бути менше або дорівнювати даті початку');
        return false;
    }

    return true;
}
