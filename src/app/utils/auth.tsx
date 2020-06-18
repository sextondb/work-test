import { useRouter } from "next/router";
import Router from "next/router";

let userIsLoggedIn = false;
let username: string | undefined = undefined;

export function useAuth() {
    if (!userIsLoggedIn && typeof window !== "undefined") {
        useRouter().push('/signin')
    }
    return userIsLoggedIn;
}

export function login(newUsername: string) {
    if (!newUsername || newUsername.trim().length === 0) {
        return;
    }

    userIsLoggedIn = true;
    username = newUsername;
    Router.push('/')
}

export function logout() {
    userIsLoggedIn = false;
    username = undefined;
    Router.push('/')
}

export function getUsername() {
    return username;
}