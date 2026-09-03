import { computed } from '@angular/core';
import { signalStore, withState, withMethods, patchState, withComputed } from '@ngrx/signals';

export const GlobalStore = signalStore(
  { providedIn: 'root' },
  // Tracks active operations using a counter to handle concurrent requests
  withState({ activeRequests: 0 }),
  withMethods((store) => ({
    startLoading(): void {
      patchState(store, (state) => ({ activeRequests: state.activeRequests + 1 }));
    },
    stopLoading(): void {
      patchState(store, (state) => ({ 
        activeRequests: Math.max(0, state.activeRequests - 1) 
      }));
    },
  })),
  // Derived state: loading is true if there is at least one active request
  withComputed((store) => ({
    isLoading: computed(() => store.activeRequests() > 0),
  }))
);
