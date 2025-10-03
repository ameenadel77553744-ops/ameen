// Saudi Electronic Market 2030 - Premium JavaScript

// Premium Toast Notification System
class PremiumToast {
    constructor() {
        this.container = this.createContainer();
        document.body.appendChild(this.container);
    }

    createContainer() {
        const container = document.createElement('div');
        container.className = 'toast-container';
        return container;
    }

    show(message, title = 'نجح!', type = 'success', duration = 4000) {
        const toast = document.createElement('div');
        toast.className = 'premium-toast';
        
        const isArabic = document.documentElement.getAttribute('dir') === 'rtl' || 
                        document.documentElement.lang === 'ar';
        
        const icon = type === 'success' ? '✅' : type === 'error' ? '❌' : 'ℹ️';
        
        toast.innerHTML = `
            <button class="toast-close" onclick="this.parentElement.remove()">×</button>
            <div class="toast-header">
                <span class="toast-icon">${icon}</span>
                <h6 class="toast-title">${title}</h6>
            </div>
            <p class="toast-message">${message}</p>
        `;

        this.container.appendChild(toast);
        
        // Trigger animation
        setTimeout(() => toast.classList.add('show'), 100);
        
        // Auto remove
        setTimeout(() => {
            toast.classList.add('hide');
            setTimeout(() => toast.remove(), 500);
        }, duration);
    }
}

// Initialize Toast System
const toast = new PremiumToast();

// AJAX Cart System
class CartManager {
    constructor() {
        this.initializeEventListeners();
    }

    initializeEventListeners() {
        // Handle add to cart buttons
        document.addEventListener('click', (e) => {
            if (e.target.matches('.add-to-cart-btn') || e.target.closest('.add-to-cart-btn')) {
                e.preventDefault();
                this.handleAddToCart(e.target.closest('.add-to-cart-btn') || e.target);
            }
        });

        // Handle forms with add-to-cart class
        document.addEventListener('submit', (e) => {
            if (e.target.matches('.add-to-cart-form')) {
                e.preventDefault();
                this.handleAddToCartForm(e.target);
            }
        });
    }

    async handleAddToCart(button) {
        const productId = button.dataset.productId || button.getAttribute('data-product-id');
        const quantity = button.dataset.quantity || 1;
        
        if (!productId) {
            console.error('Product ID not found');
            return;
        }

        await this.addToCart(productId, quantity);
    }

    async handleAddToCartForm(form) {
        const formData = new FormData(form);
        const productId = formData.get('productId');
        const quantity = formData.get('quantity') || 1;
        
        await this.addToCart(productId, quantity);
    }

    async addToCart(productId, quantity = 1) {
        try {
            // Show loading state on button
            const buttons = document.querySelectorAll(`[data-product-id="${productId}"]`);
            buttons.forEach(btn => {
                btn.classList.add('loading');
                btn.disabled = true;
            });

            const isArabic = document.documentElement.getAttribute('dir') === 'rtl' || 
                            document.documentElement.lang === 'ar';

            const response = await fetch('/Cart/Add', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'X-Requested-With': 'XMLHttpRequest',
                    'RequestVerificationToken': this.getAntiForgeryToken()
                },
                body: `productId=${productId}&quantity=${quantity}`
            });

            if (response.ok) {
                const result = await response.json();
                
                if (result.success) {
                    // Show success message
                    toast.show(
                        result.message,
                        isArabic ? '🎉 تم بنجاح!' : '🎉 Success!',
                        'success'
                    );
                    
                    // Update cart counter if exists
                    this.updateCartCounter(result.cartItemsCount);
                    
                    // Add visual feedback to button
                    this.addButtonFeedback(productId);
                } else {
                    throw new Error(result.message || 'Failed to add to cart');
                }
            } else {
                throw new Error('Network error');
            }
        } catch (error) {
            console.error('Error adding to cart:', error);
            const isArabic = document.documentElement.getAttribute('dir') === 'rtl' || 
                            document.documentElement.lang === 'ar';
            
            toast.show(
                isArabic ? 'حدث خطأ أثناء الإضافة' : 'Error adding to cart',
                isArabic ? 'خطأ!' : 'Error!',
                'error'
            );
        } finally {
            // Remove loading state
            const buttons = document.querySelectorAll(`[data-product-id="${productId}"]`);
            buttons.forEach(btn => {
                btn.classList.remove('loading');
                btn.disabled = false;
            });
        }
    }

    getAntiForgeryToken() {
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        return token ? token.value : '';
    }

    updateCartCounter(count) {
        const counters = document.querySelectorAll('.cart-counter, .cart-count, .badge');
        counters.forEach(counter => {
            if (counter.textContent.match(/^\d+$/) || counter.textContent.trim() === '') {
                counter.textContent = count;
                // Add premium animation
                counter.classList.add('cart-counter-pulse');
                setTimeout(() => counter.classList.remove('cart-counter-pulse'), 600);
            }
        });
    }

    addButtonFeedback(productId) {
        const buttons = document.querySelectorAll(`[data-product-id="${productId}"]`);
        buttons.forEach(button => {
            const originalText = button.textContent;
            const isArabic = document.documentElement.getAttribute('dir') === 'rtl' || 
                            document.documentElement.lang === 'ar';
            
            // Add success state
            button.classList.add('success');
            button.textContent = isArabic ? 'تمت الإضافة' : 'Added';
            
            setTimeout(() => {
                button.classList.remove('success');
                button.textContent = originalText;
            }, 2500);
        });
    }
}

// Initialize Systems
window.addEventListener('DOMContentLoaded', function () {
    // Initialize AOS animations
    if (window.AOS) {
        AOS.init({
            duration: 700,
            once: true,
            easing: 'ease-out-quart'
        });
    }
    
    // Initialize Cart Manager
    new CartManager();
    
    // Add premium loading effects
    document.body.classList.add('loaded');
});

// Export for global access
window.SaudiMarket = {
    toast,
    CartManager
};
