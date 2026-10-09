# MudBlazor Lab Staff Console — Design

**Status:** Proposed UI/API integration design; no production behavior or role has been added.

## Scope

Add a dedicated Lab Staff web area for lab-computer operations. It is distinct from SAO and Admissions. Do not add Guidance scope, and do not move student or guard workflows into the web app.

The existing login service currently expects a direct token and role, while the API MFA login flow is two-step. Update the shared login service and login page to handle the actual MFA setup/verify response before granting navigation. Never persist a JWT before MFA verification succeeds. Recovery codes appear once after initial setup; later verification may use one code.

## Lab Staff screens

1. **Lab overview** — enabled/offline PCs, active sessions, vouchers expiring soon, and clear empty/loading/error states.
2. **Computers** — list PC name/location/status/last heartbeat; register a PC and show its one-time enrollment code only once; disable a PC with confirmation.
3. **Issue voucher** — find/select a student by student number, choose a policy-bounded duration (default 60 minutes), issue voucher, show the raw code once with a copy action and a warning to share it through the approved school channel.
4. **Vouchers** — filter by student/status/date, show issued/expiry/redeemed/revoked state, revoke an unused voucher.
5. **Active sessions** — show student, PC, start/expiry, heartbeat status; end a session with confirmation and an audit reason.

Raw enrollment and voucher codes must not be shown again after the initial response or written to browser storage/logs. The browser must not hold any PC agent credential. Destructive actions require clear confirmation and server authorization.

## Navigation and authorization

SAO has a separate account-provisioning page that creates LabStaff accounts with a temporary password shown once; it does not grant those accounts SAO or Admissions access. Add a Lab Staff route and role-aware navigation only after the API recognizes the explicit `LabStaff` role. Directly visiting the route must show access denied for any other role. The API remains the authority; hiding a menu item is not authorization. LabStaff receives no SAO or Admissions access. Existing areas remain separate.

## API integration

Use a typed Lab PC service with bearer-authenticated requests to the LabStaff management endpoints from the API design. Parse the standard `{ status, message, data }` envelope; display useful messages for 401/403/404/409/429 and network errors. Do not assume responses contain optional null-valued fields. Refresh the PC/session state from the server; do not trust a browser countdown as the session authority.

MFA service changes must preserve the current register behavior and logout semantics. Sign out clears token and role and returns to the correct login route. Avoid unrelated redesign of existing SAO/Admissions pages.

## Security and usability

- Use server-issued student search data, not arbitrary client-submitted identity claims.
- Do not expose voucher lists or student data to unauthenticated users or other roles.
- Show enrollment/voucher one-time secrets only at issuance; prevent accidental reuse and mask after leaving the result screen.
- Make active/offline state understandable and offer refresh.
- Keep layout responsive for desktop staff devices; keyboard navigation and accessible labels are required.

## Acceptance checklist

- Login supports first-time authenticator setup, normal code verification, and recovery-code use before saving JWT.
- Only LabStaff can access computer/voucher/session management routes; SAO and Admissions remain in their own areas.
- Voucher issuance is student-bound, duration-bounded, and shown once.
- PC enrollment secret is shown once; disabled/offline PCs are clearly marked.
- Session end and voucher revoke have confirmation and clear result state.
- API authorization remains enforced even when a non-LabStaff user manually opens a route.
- Existing SAO and Admissions dashboard behavior is not folded into the Lab Staff console.
