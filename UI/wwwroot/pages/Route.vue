<template lang="html">
	<div class="h-100">
		<topbar></topbar>
		<div class="container-fluid">
			<main role="main" class="main-content px-md-4 pt-4 pb-5">
				<router-view></router-view>
			</main>
		</div>
		<footer-site></footer-site>
	</div>
</template>

<script>
module.exports = {
    components: {
		Topbar: httpVueLoader('components/Topbar.vue'),
		FooterSite: httpVueLoader('components/Footer.vue')
    }
}
</script>
