<template lang="html">
    <footer class="sticky-footer">
        <div class="copyright text-left my-auto" style="width: 100%;padding: 0 20px;">
            <img src="../images/deckcards.png" height="22" >
        </div>
    </footer>
</template>

<script>
    module.exports = {
        data() {
            return {
            };
        }
    }
</script>

<style lang="css" scoped>
    @media (min-width: 768px) {
        body.sidebar-toggled footer.sticky-footer, footer.sticky-footer {
            width: 100%;
        }
    }

    @media (min-width: 768px) {
        body.sidebar-toggled footer.sticky-footer {
            width: calc(100% - 90px);
        }
    }

    body.sidebar-toggled footer.sticky-footer {
        width: 100%;
    }

    @media (min-width: 768px) {
        footer.sticky-footer {
            width: calc(100% - 225px);
        }
    }

    footer.sticky-footer {
        display: -webkit-box;
        display: -ms-flexbox;
        display: flex;
        position: fixed;
        right: 0;
        bottom: 0;
        width: 100%;
        height: 30px;
        background-color: black;
    }
    .pull-right {
        float: right;
    }
</style>

