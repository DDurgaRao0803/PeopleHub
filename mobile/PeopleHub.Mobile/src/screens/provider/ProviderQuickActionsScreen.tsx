import React from "react";
import {
  Pressable,
  ScrollView,
  StyleSheet,
  Text,
  View,
} from "react-native";
import { Ionicons } from "@expo/vector-icons";
import { useNavigation } from "@react-navigation/native";

import {
  colors,
  radius,
  shadows,
  spacing,
  typography,
} from "../../theme";

const actions = [
  // Manage Business
  {
    title: "View Requests",
    subtitle: "View & manage",
    icon: "briefcase",
    iconColor: "#16A34A",
    iconBackground: "#ECFDF5",
  },
  {
    title: "My Services",
    subtitle: "Manage services",
    icon: "construct",
    iconColor: "#2563EB",
    iconBackground: "#EFF6FF",
  },
  {
    title: "Availability",
    subtitle: "Working hours",
    icon: "time",
    iconColor: "#7C3AED",
    iconBackground: "#F3E8FF",
  },
  {
    title: "Calendar",
    subtitle: "Bookings",
    icon: "calendar",
    iconColor: "#EF4444",
    iconBackground: "#FEF2F2",
  },

  // Earnings
  {
    title: "Today's Earnings",
    subtitle: "Today's income",
    icon: "cash",
    iconColor: "#059669",
    iconBackground: "#ECFDF5",
  },
  {
    title: "Weekly Earnings",
    subtitle: "Weekly report",
    icon: "stats-chart",
    iconColor: "#2563EB",
    iconBackground: "#EFF6FF",
  },
  {
    title: "Wallet",
    subtitle: "Transactions",
    icon: "wallet",
    iconColor: "#EA580C",
    iconBackground: "#FFF7ED",
  },

  // Growth
  {
    title: "Reviews",
    subtitle: "Customer feedback",
    icon: "star",
    iconColor: "#F59E0B",
    iconBackground: "#FFF7ED",
  },
  {
    title: "Documents",
    subtitle: "Verification",
    icon: "shield-checkmark",
    iconColor: "#10B981",
    iconBackground: "#ECFDF5",
  },

  // Support
  {
  title: "Support",
  subtitle: "Help Center & Support",
  icon: "headset",
  iconColor: "#8B5CF6",
  iconBackground: "#F3E8FF",
},
];

export default function ProviderQuickActionsScreen(): React.JSX.Element {
    const navigation = useNavigation();
    
  return (
  <ScrollView
    style={styles.container}
    contentContainerStyle={styles.content}
    showsVerticalScrollIndicator={false}
  >

    <View style={styles.header}>
  <Pressable onPress={() => navigation.goBack()}>
    <Ionicons
      name="arrow-back"
      size={24}
      color={colors.text.primary}
    />
  </Pressable>

  <Text style={styles.headerTitle}>
    Quick Actions
  </Text>

  <View style={{ width: 24 }} />
</View>


    {/* Hero Banner */}
    <View style={styles.heroBanner}>
      <View style={styles.heroContent}>
        <Text style={styles.heroTitle}>
          Everything you need
        </Text>

        <Text style={styles.heroSubtitle}>
          Manage your business{"\n"}from one place
        </Text>
      </View>

      <View style={styles.heroImage}>
        <Ionicons
    name="construct"
    size={56}
    color="#FFFFFF"
/>
      </View>
    </View>

    {/* Manage Business */}
    <Text style={styles.sectionTitle}>
      Manage Business
    </Text>

    <View style={styles.grid}>
      {actions.slice(0, 4).map((item) => (
        <Pressable
          key={item.title}
          style={styles.smallCard}
          onPress={() => {}}
        >
          <View
  style={[
    styles.smallIcon,
    { backgroundColor: item.iconBackground },
  ]}
>
            <Ionicons
    name={item.icon as any}
    size={24}
    color={item.iconColor}
/>
          </View>

          <Text style={styles.smallTitle}>
            {item.title}
          </Text>

          <Text style={styles.smallSubtitle}>
            {item.subtitle}
          </Text>
        </Pressable>
      ))}
    </View>

    {/* Earnings & Finance */}

<Text style={styles.sectionTitle}>
  Earnings & Finance
</Text>

<View style={styles.financeGrid}>
  {actions.slice(4, 7).map((item) => (
    <Pressable
      key={item.title}
      style={styles.financeCard}
    >
      <View
  style={[
    styles.financeIcon,
    { backgroundColor: item.iconBackground },
  ]}
>
        <Ionicons
    name={item.icon as any}
    size={24}
    color={item.iconColor}
/>
      </View>

      <Text style={styles.financeTitle}>
        {item.title}
      </Text>

      <Text style={styles.financeSubtitle}>
        {item.subtitle}
      </Text>
    </Pressable>
  ))}
</View>

{/* Growth & Trust */}

<Text style={styles.sectionTitle}>
  Growth & Trust
</Text>

<View style={styles.growthRow}>
  {actions.slice(7, 9).map((item) => (
    <Pressable
      key={item.title}
      style={styles.growthCard}
    >
      <View
  style={[
    styles.growthIcon,
    { backgroundColor: item.iconBackground },
  ]}
>
        <Ionicons
    name={item.icon as any}
    size={22}
    color={item.iconColor}
/>
      </View>

      <Text style={styles.growthTitle}>
        {item.title}
      </Text>

      <Text style={styles.growthSubtitle}>
        {item.subtitle}
      </Text>

      <Ionicons
        name="chevron-forward"
        size={18}
        color="#94A3B8"
        style={styles.chevron}
      />
    </Pressable>
  ))}
</View>

{/* Support */}

<Text style={styles.sectionTitle}>
  Support
</Text>

<Pressable style={styles.supportCard}>
  <View style={styles.supportLeft}>
    <View
  style={[
    styles.supportIcon,
    {
      backgroundColor: actions[9].iconBackground,
    },
  ]}
>
      <Ionicons
    name={actions[9].icon as any}
    size={24}
    color={actions[9].iconColor}
/>
    </View>

    <View style={styles.supportTextContainer}>
      <Text style={styles.supportTitle}>
  {actions[9].title}
</Text>

      <Text style={styles.supportSubtitle}>
  {actions[9].subtitle}
</Text>
    </View>
  </View>

  <Ionicons
    name="chevron-forward"
    size={20}
    color="#94A3B8"
  />
</Pressable>

  </ScrollView>
);
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#F8FAFC",
  },

  content: {
    padding: spacing.lg,
  },

  card: {
    height: 70,
    backgroundColor: "#FFFFFF",
    borderRadius: radius.lg,
    paddingHorizontal: spacing.lg,
    marginBottom: spacing.md,

    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",

    ...shadows.sm,
  },

  left: {
    flexDirection: "row",
    alignItems: "center",
  },

  iconContainer: {
    width: 42,
    height: 42,
    borderRadius: 12,
    justifyContent: "center",
    alignItems: "center",
    marginRight: spacing.md,
  },

  title: {
    ...typography.body,
    color: colors.text.primary,
    fontWeight: "600",
  },

  heroBanner: {
  backgroundColor: "#16A34A",
  borderRadius: 20,
  padding: 20,
  marginBottom: 28,

  flexDirection: "row",
  justifyContent: "space-between",
  alignItems: "center",
},

heroContent: {
  flex: 1,
},

heroTitle: {
  color: "#FFFFFF",
  fontSize: 22,
  fontWeight: "700",
},

heroSubtitle: {
  color: "#E8F5E9",
  marginTop: 8,
  fontSize: 15,
  lineHeight: 22,
},

heroImage: {
  width: 90,
  height: 90,
  borderRadius: 45,
  backgroundColor: "rgba(255,255,255,0.15)",

  justifyContent: "center",
  alignItems: "center",
},

sectionTitle: {
  ...typography.h3,
  marginBottom: 18,
  color: colors.text.primary,
},

grid: {
  flexDirection: "row",
  justifyContent: "space-between",
  marginBottom: 30,
},

smallCard: {
  width: "23%",
  backgroundColor: "#FFFFFF",
  borderRadius: 18,
  paddingVertical: 14,
  paddingHorizontal: 8,
  alignItems: "center",

  ...shadows.sm,
},

smallIcon: {
  width: 40,
  height: 40,
  borderRadius: 12,
  justifyContent: "center",
  alignItems: "center",
  marginBottom: 10,
},

smallTitle: {
  fontSize: 12,
  fontWeight: "700",
  textAlign: "center",
  color: colors.text.primary,
},

smallSubtitle: {
  marginTop: 6,
  fontSize: 10,
  lineHeight: 14,
  color: "#64748B",
  textAlign: "center",
},

financeGrid: {
  flexDirection: "row",
  justifyContent: "space-between",
  marginBottom: 28,
},

financeCard: {
  width: "31%",
  backgroundColor: "#FFFFFF",
  borderRadius: 18,
  padding: 14,
  alignItems: "center",
  ...shadows.sm,
},

financeIcon: {
  width: 42,
  height: 42,
  borderRadius: 12,
  justifyContent: "center",
  alignItems: "center",
  marginBottom: 10,
},

financeTitle: {
  fontSize: 12,
  fontWeight: "700",
  textAlign: "center",
},

financeSubtitle: {
  marginTop: 6,
  fontSize: 10,
  color: "#64748B",
  textAlign: "center",
  lineHeight: 14,
},

growthRow: {
  flexDirection: "row",
  justifyContent: "space-between",
  marginBottom: 28,
},

growthCard: {
  width: "48%",
  backgroundColor: "#FFFFFF",
  borderRadius: 18,
  padding: 16,
  ...shadows.sm,
},

growthIcon: {
  width: 42,
  height: 42,
  borderRadius: 12,
  justifyContent: "center",
  alignItems: "center",
  marginBottom: 12,
},

growthTitle: {
  fontSize: 15,
  fontWeight: "700",
  color: colors.text.primary,
},

growthSubtitle: {
  marginTop: 6,
  fontSize: 12,
  color: "#64748B",
  lineHeight: 18,
},

chevron: {
  position: "absolute",
  right: 16,
  bottom: 16,
},

supportCard: {
  backgroundColor: "#FFFFFF",
  borderRadius: 18,
  paddingHorizontal: 16,
  paddingVertical: 16,

  flexDirection: "row",
  justifyContent: "space-between",
  alignItems: "center",

  marginBottom: 30,

  ...shadows.sm,
},

supportLeft: {
  flexDirection: "row",
  alignItems: "center",
},

supportIcon: {
  width: 44,
  height: 44,
  borderRadius: 12,
  backgroundColor: "#F3E8FF",
  justifyContent: "center",
  alignItems: "center",
  marginRight: 14,
},

supportTextContainer: {
  justifyContent: "center",
},

supportTitle: {
  fontSize: 15,
  fontWeight: "700",
  color: colors.text.primary,
},

supportSubtitle: {
  marginTop: 4,
  fontSize: 12,
  color: "#64748B",
},

header: {
  paddingTop: spacing.md,
  height: 64,
  flexDirection: "row",
  alignItems: "center",
  justifyContent: "space-between",
  marginBottom: 20,
},

headerTitle: {
  fontSize: 20,
  fontWeight: "700",
  color: colors.text.primary,
},


});